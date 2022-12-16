'use strict';

var utcDate = '2022-12-16T03:38:29.703'
this.ConvertUTCtoLocalTime = function (utcDate) {
    if (utcDate != null) {
        var date = moment.utc(utcDate);
        date.local();
        return date.format("MM/DD/YYYY hh:mm:ss A");
    }
    else
        return "";
};

console.log('Hello world');
