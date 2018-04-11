var System = (function () { // @namespace
    function System() {
    };
    System.Console = (function() { // @class
        function Console() {
        };
        Console.WriteLine = function(message) {
            console.log(message);
        };
        return Console;
    }());
    return System;
}());