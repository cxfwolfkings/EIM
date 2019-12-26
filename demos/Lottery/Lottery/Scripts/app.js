; (function () {
    var app = {
        getQueryVariable: function (variable) {
            var query = window.location.search.substring(1);
            var vars = query.split("&");
            for (var i = 0; i < vars.length; i++) {
                var pair = vars[i].split("=");
                if (pair[0] === variable) { return pair[1]; }
            }
            return false;
        },
        getRandom: function () {
            return Math.ceil(Math.random() * 101);
        },
        getMatrix: function (itemId) {
            var el = document.getElementById(itemId);
            var st = window.getComputedStyle(el, null);
            var tr = st.getPropertyValue("-webkit-transform") ||
                st.getPropertyValue("-moz-transform") ||
                st.getPropertyValue("-ms-transform") ||
                st.getPropertyValue("-o-transform") ||
                st.getPropertyValue("transform") ||
                "FAIL";
            // With rotate(30deg)...
            // matrix(0.866025, 0.5, -0.5, 0.866025, 0px, 0px)
            console.log('Matrix: ' + tr);
            // rotation matrix - http://en.wikipedia.org/wiki/Rotation_matrix
            var values = tr.split('(')[1].split(')')[0].split(',');
            var a = values[0];
            var b = values[1];
            var c = values[2];
            var d = values[3];
            var scale = Math.sqrt(a * a + b * b);
            console.log('Scale: ' + scale);
            // arc sin, convert from radians to degrees, round
            var sin = b / scale;
            // next line works for 30deg but not 130deg (returns 50);
            // var angle = Math.round(Math.asin(sin) * (180/Math.PI));
            var angle = Math.round(Math.atan2(b, a) * (180 / Math.PI));
            console.log('Rotate: ' + angle + 'deg');
        },
        getRotate: function (itemId) {
            var el = document.getElementById(itemId);
            var st = window.getComputedStyle(el, null);
            var tr = st.getPropertyValue("-webkit-transform") ||
                st.getPropertyValue("-moz-transform") ||
                st.getPropertyValue("-ms-transform") ||
                st.getPropertyValue("-o-transform") ||
                st.getPropertyValue("transform") ||
                "FAIL";
            var values = tr.split('(')[1].split(')')[0].split(',');
            var a = values[0];
            var b = values[1];
            var angle = Math.round(Math.atan2(b, a) * (180 / Math.PI));
            return angle;
        }
    };
    window.app = app;
})();
