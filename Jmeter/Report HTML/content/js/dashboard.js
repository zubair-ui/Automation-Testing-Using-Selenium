/*
   Licensed to the Apache Software Foundation (ASF) under one or more
   contributor license agreements.  See the NOTICE file distributed with
   this work for additional information regarding copyright ownership.
   The ASF licenses this file to You under the Apache License, Version 2.0
   (the "License"); you may not use this file except in compliance with
   the License.  You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/
var showControllersOnly = false;
var seriesFilter = "";
var filtersOnlySampleSeries = true;

/*
 * Add header in statistics table to group metrics by category
 * format
 *
 */
function summaryTableHeader(header) {
    var newRow = header.insertRow(-1);
    newRow.className = "tablesorter-no-sort";
    var cell = document.createElement('th');
    cell.setAttribute("data-sorter", false);
    cell.colSpan = 1;
    cell.innerHTML = "Requests";
    newRow.appendChild(cell);

    cell = document.createElement('th');
    cell.setAttribute("data-sorter", false);
    cell.colSpan = 3;
    cell.innerHTML = "Executions";
    newRow.appendChild(cell);

    cell = document.createElement('th');
    cell.setAttribute("data-sorter", false);
    cell.colSpan = 7;
    cell.innerHTML = "Response Times (ms)";
    newRow.appendChild(cell);

    cell = document.createElement('th');
    cell.setAttribute("data-sorter", false);
    cell.colSpan = 1;
    cell.innerHTML = "Throughput";
    newRow.appendChild(cell);

    cell = document.createElement('th');
    cell.setAttribute("data-sorter", false);
    cell.colSpan = 2;
    cell.innerHTML = "Network (KB/sec)";
    newRow.appendChild(cell);
}

/*
 * Populates the table identified by id parameter with the specified data and
 * format
 *
 */
function createTable(table, info, formatter, defaultSorts, seriesIndex, headerCreator) {
    var tableRef = table[0];

    // Create header and populate it with data.titles array
    var header = tableRef.createTHead();

    // Call callback is available
    if(headerCreator) {
        headerCreator(header);
    }

    var newRow = header.insertRow(-1);
    for (var index = 0; index < info.titles.length; index++) {
        var cell = document.createElement('th');
        cell.innerHTML = info.titles[index];
        newRow.appendChild(cell);
    }

    var tBody;

    // Create overall body if defined
    if(info.overall){
        tBody = document.createElement('tbody');
        tBody.className = "tablesorter-no-sort";
        tableRef.appendChild(tBody);
        var newRow = tBody.insertRow(-1);
        var data = info.overall.data;
        for(var index=0;index < data.length; index++){
            var cell = newRow.insertCell(-1);
            cell.innerHTML = formatter ? formatter(index, data[index]): data[index];
        }
    }

    // Create regular body
    tBody = document.createElement('tbody');
    tableRef.appendChild(tBody);

    var regexp;
    if(seriesFilter) {
        regexp = new RegExp(seriesFilter, 'i');
    }
    // Populate body with data.items array
    for(var index=0; index < info.items.length; index++){
        var item = info.items[index];
        if((!regexp || filtersOnlySampleSeries && !info.supportsControllersDiscrimination || regexp.test(item.data[seriesIndex]))
                &&
                (!showControllersOnly || !info.supportsControllersDiscrimination || item.isController)){
            if(item.data.length > 0) {
                var newRow = tBody.insertRow(-1);
                for(var col=0; col < item.data.length; col++){
                    var cell = newRow.insertCell(-1);
                    cell.innerHTML = formatter ? formatter(col, item.data[col]) : item.data[col];
                }
            }
        }
    }

    // Add support of columns sort
    table.tablesorter({sortList : defaultSorts});
}

$(document).ready(function() {

    // Customize table sorter default options
    $.extend( $.tablesorter.defaults, {
        theme: 'blue',
        cssInfoBlock: "tablesorter-no-sort",
        widthFixed: true,
        widgets: ['zebra']
    });

    var data = {"OkPercent": 95.5, "KoPercent": 4.5};
    var dataset = [
        {
            "label" : "FAIL",
            "data" : data.KoPercent,
            "color" : "#FF6347"
        },
        {
            "label" : "PASS",
            "data" : data.OkPercent,
            "color" : "#9ACD32"
        }];
    $.plot($("#flot-requests-summary"), dataset, {
        series : {
            pie : {
                show : true,
                radius : 1,
                label : {
                    show : true,
                    radius : 3 / 4,
                    formatter : function(label, series) {
                        return '<div style="font-size:8pt;text-align:center;padding:2px;color:white;">'
                            + label
                            + '<br/>'
                            + Math.round10(series.percent, -2)
                            + '%</div>';
                    },
                    background : {
                        opacity : 0.5,
                        color : '#000'
                    }
                }
            }
        },
        legend : {
            show : true
        }
    });

    // Creates APDEX table
    createTable($("#apdexTable"), {"supportsControllersDiscrimination": true, "overall": {"data": [0.84125, 500, 1500, "Total"], "isController": false}, "titles": ["Apdex", "T (Toleration threshold)", "F (Frustration threshold)", "Label"], "items": [{"data": [0.97, 500, 1500, "DELETE"], "isController": false}, {"data": [0.97, 500, 1500, "POST"], "isController": false}, {"data": [0.495, 500, 1500, "GET "], "isController": false}, {"data": [0.93, 500, 1500, "PUT"], "isController": false}]}, function(index, item){
        switch(index){
            case 0:
                item = item.toFixed(3);
                break;
            case 1:
            case 2:
                item = formatDuration(item);
                break;
        }
        return item;
    }, [[0, 0]], 3);

    // Create statistics table
    createTable($("#statisticsTable"), {"supportsControllersDiscrimination": true, "overall": {"data": ["Total", 400, 18, 4.5, 426.17499999999995, 319, 1873, 351.0, 575.9000000000001, 678.8499999999999, 1257.5500000000004, 0.37343181975192924, 0.5935067879402062, 0.07952201427256415], "isController": false}, "titles": ["Label", "#Samples", "FAIL", "Error %", "Average", "Min", "Max", "Median", "90th pct", "95th pct", "99th pct", "Transactions/s", "Received", "Sent"], "items": [{"data": ["DELETE", 100, 3, 3.0, 366.7000000000001, 320, 1044, 343.0, 398.9, 438.79999999999995, 1042.7299999999993, 0.093481882276396, 0.20760098555611434, 0.019304373853678416], "isController": false}, {"data": ["POST", 100, 3, 3.0, 361.8799999999998, 319, 919, 341.5, 419.4000000000001, 449.4999999999999, 916.0299999999985, 0.0934825813906095, 0.07049536069319204, 0.026547592449598863], "isController": false}, {"data": ["GET ", 100, 5, 5.0, 621.9100000000003, 492, 1873, 530.5, 771.4000000000001, 1209.9499999999994, 1871.8599999999994, 0.09345017096708778, 0.20242000606958863, 0.011449471142119956], "isController": false}, {"data": ["PUT", 100, 7, 7.0, 354.21000000000004, 322, 617, 340.5, 385.6, 446.4999999999999, 617.0, 0.09348380486564507, 0.11371245865678731, 0.022322910122800327], "isController": false}]}, function(index, item){
        switch(index){
            // Errors pct
            case 3:
                item = item.toFixed(2) + '%';
                break;
            // Mean
            case 4:
            // Mean
            case 7:
            // Median
            case 8:
            // Percentile 1
            case 9:
            // Percentile 2
            case 10:
            // Percentile 3
            case 11:
            // Throughput
            case 12:
            // Kbytes/s
            case 13:
            // Sent Kbytes/s
                item = item.toFixed(2);
                break;
        }
        return item;
    }, [[0, 0]], 0, summaryTableHeader);

    // Create error table
    createTable($("#errorsTable"), {"supportsControllersDiscrimination": false, "titles": ["Type of error", "Number of errors", "% in errors", "% in all samples"], "items": [{"data": ["The operation lasted too long: It took 622 milliseconds, but should not have lasted longer than 500 milliseconds.", 1, 5.555555555555555, 0.25], "isController": false}, {"data": ["The operation lasted too long: It took 417 milliseconds, but should not have lasted longer than 400 milliseconds.", 1, 5.555555555555555, 0.25], "isController": false}, {"data": ["The operation lasted too long: It took 1,759 milliseconds, but should not have lasted longer than 1,200 milliseconds.", 1, 5.555555555555555, 0.25], "isController": false}, {"data": ["The operation lasted too long: It took 1,044 milliseconds, but should not have lasted longer than 500 milliseconds.", 1, 5.555555555555555, 0.25], "isController": false}, {"data": ["The operation lasted too long: It took 919 milliseconds, but should not have lasted longer than 500 milliseconds.", 1, 5.555555555555555, 0.25], "isController": false}, {"data": ["The operation lasted too long: It took 1,258 milliseconds, but should not have lasted longer than 1,200 milliseconds.", 1, 5.555555555555555, 0.25], "isController": false}, {"data": ["The operation lasted too long: It took 477 milliseconds, but should not have lasted longer than 400 milliseconds.", 1, 5.555555555555555, 0.25], "isController": false}, {"data": ["The operation lasted too long: It took 917 milliseconds, but should not have lasted longer than 500 milliseconds.", 1, 5.555555555555555, 0.25], "isController": false}, {"data": ["The operation lasted too long: It took 437 milliseconds, but should not have lasted longer than 400 milliseconds.", 1, 5.555555555555555, 0.25], "isController": false}, {"data": ["The operation lasted too long: It took 1,325 milliseconds, but should not have lasted longer than 1,200 milliseconds.", 1, 5.555555555555555, 0.25], "isController": false}, {"data": ["The operation lasted too long: It took 617 milliseconds, but should not have lasted longer than 500 milliseconds.", 1, 5.555555555555555, 0.25], "isController": false}, {"data": ["The operation lasted too long: It took 537 milliseconds, but should not have lasted longer than 400 milliseconds.", 1, 5.555555555555555, 0.25], "isController": false}, {"data": ["The operation lasted too long: It took 1,213 milliseconds, but should not have lasted longer than 1,200 milliseconds.", 1, 5.555555555555555, 0.25], "isController": false}, {"data": ["The operation lasted too long: It took 1,873 milliseconds, but should not have lasted longer than 1,200 milliseconds.", 1, 5.555555555555555, 0.25], "isController": false}, {"data": ["The operation lasted too long: It took 617 milliseconds, but should not have lasted longer than 400 milliseconds.", 1, 5.555555555555555, 0.25], "isController": false}, {"data": ["The operation lasted too long: It took 616 milliseconds, but should not have lasted longer than 500 milliseconds.", 1, 5.555555555555555, 0.25], "isController": false}, {"data": ["The operation lasted too long: It took 447 milliseconds, but should not have lasted longer than 400 milliseconds.", 1, 5.555555555555555, 0.25], "isController": false}, {"data": ["The operation lasted too long: It took 613 milliseconds, but should not have lasted longer than 500 milliseconds.", 1, 5.555555555555555, 0.25], "isController": false}]}, function(index, item){
        switch(index){
            case 2:
            case 3:
                item = item.toFixed(2) + '%';
                break;
        }
        return item;
    }, [[1, 1]]);

        // Create top5 errors by sampler
    createTable($("#top5ErrorsBySamplerTable"), {"supportsControllersDiscrimination": false, "overall": {"data": ["Total", 400, 18, "The operation lasted too long: It took 622 milliseconds, but should not have lasted longer than 500 milliseconds.", 1, "The operation lasted too long: It took 417 milliseconds, but should not have lasted longer than 400 milliseconds.", 1, "The operation lasted too long: It took 1,759 milliseconds, but should not have lasted longer than 1,200 milliseconds.", 1, "The operation lasted too long: It took 1,044 milliseconds, but should not have lasted longer than 500 milliseconds.", 1, "The operation lasted too long: It took 919 milliseconds, but should not have lasted longer than 500 milliseconds.", 1], "isController": false}, "titles": ["Sample", "#Samples", "#Errors", "Error", "#Errors", "Error", "#Errors", "Error", "#Errors", "Error", "#Errors", "Error", "#Errors"], "items": [{"data": ["DELETE", 100, 3, "The operation lasted too long: It took 1,044 milliseconds, but should not have lasted longer than 500 milliseconds.", 1, "The operation lasted too long: It took 917 milliseconds, but should not have lasted longer than 500 milliseconds.", 1, "The operation lasted too long: It took 616 milliseconds, but should not have lasted longer than 500 milliseconds.", 1, "", "", "", ""], "isController": false}, {"data": ["POST", 100, 3, "The operation lasted too long: It took 622 milliseconds, but should not have lasted longer than 500 milliseconds.", 1, "The operation lasted too long: It took 919 milliseconds, but should not have lasted longer than 500 milliseconds.", 1, "The operation lasted too long: It took 613 milliseconds, but should not have lasted longer than 500 milliseconds.", 1, "", "", "", ""], "isController": false}, {"data": ["GET ", 100, 5, "The operation lasted too long: It took 1,213 milliseconds, but should not have lasted longer than 1,200 milliseconds.", 1, "The operation lasted too long: It took 1,759 milliseconds, but should not have lasted longer than 1,200 milliseconds.", 1, "The operation lasted too long: It took 1,873 milliseconds, but should not have lasted longer than 1,200 milliseconds.", 1, "The operation lasted too long: It took 1,258 milliseconds, but should not have lasted longer than 1,200 milliseconds.", 1, "The operation lasted too long: It took 1,325 milliseconds, but should not have lasted longer than 1,200 milliseconds.", 1], "isController": false}, {"data": ["PUT", 100, 7, "The operation lasted too long: It took 617 milliseconds, but should not have lasted longer than 500 milliseconds.", 1, "The operation lasted too long: It took 417 milliseconds, but should not have lasted longer than 400 milliseconds.", 1, "The operation lasted too long: It took 537 milliseconds, but should not have lasted longer than 400 milliseconds.", 1, "The operation lasted too long: It took 477 milliseconds, but should not have lasted longer than 400 milliseconds.", 1, "The operation lasted too long: It took 617 milliseconds, but should not have lasted longer than 400 milliseconds.", 1], "isController": false}]}, function(index, item){
        return item;
    }, [[0, 0]], 0);

});
