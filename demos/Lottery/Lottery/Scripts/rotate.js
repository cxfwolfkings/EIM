!(function () {
    class CanvasDoms {
        constructor() {
            this.canvasOut1 = null;
            this.canvasOut2 = null;
            this.canvasOut3 = null;
            this.canvasIn1 = null;
            this.canvasIn2 = null;
            this.canvasPoint = null;
        }
    }

    class CanvasCtxs {
        constructor(doms) {
            var canvases = new CanvasDoms();
            canvases = Object.assign(canvases, doms);
            this.ctxOut1 = canvases.canvasOut1.getContext('2d');
            this.ctxOut2 = canvases.canvasOut2.getContext('2d');
            this.ctxOut3 = canvases.canvasOut3.getContext('2d');
            if (canvases.canvasIn1) {
                this.ctxIn1 = canvases.canvasIn1.getContext('2d');
            }
            if (canvases.canvasIn2) {
                this.ctxIn2 = canvases.canvasIn2.getContext('2d');
            }
            if (canvases.canvasPoint) {
                this.ctxPoint = canvases.canvasPoint.getContext('2d');
            }
        }
    }

    var lottery = function (canvasDoms) {
        var canvases = new CanvasDoms();
        canvases = Object.assign(canvases, canvasDoms);
        this.canvasDoms = canvases;
        this.canvasCtxs = new CanvasCtxs(canvases);

        // 扇形的背景颜色交替
        this.color = [];

        // 外部跑马灯
        // 定时器名称
        this.mytime = '';
        // 闪烁标记
        this.lamp = 0;
        // 大转盘等分数
        this.itemsNum = 0;
        // 每个扇形中的文字填充
        this.text = [];
        this.isRotate = 0;

        // 内部跑马灯
        // 闪烁标记
        this.lampIn = 0;
        this.textIn = [];
        this.mytimeIn = '';
        this.isRotateIn = 0;
    };

    lottery.prototype.init = function (num, colors, textArray, isRotate, inNum, textInArr, isRotateIn) {
        // 大转盘等分数
        this.itemsNum = num;
        // 扇形的背景颜色交替
        this.color = colors;
        // 每个扇形中的文字填充
        this.text = textArray;
        // 旋转前角度
        this.isRotate = isRotate;

        // 小转盘等分数
        this.itemsInNum = inNum;
        this.textIn = textInArr;
        this.isRotateIn = isRotateIn;

        this.getCanvasOut1();
        this.getCanvasOut2();
        this.getCanvasOut3();
        if (this.canvasDoms.canvasIn1) {
            this.getCanvasIn1();
        }
        if (this.canvasDoms.canvasIn2) {
            this.getCanvasIn2();
            // 双盘时画中轴线
            this.drawLine();
        }
    };

    // 转盘内部绘制
    lottery.prototype.getCanvasOut1 = function () {
        // 获取大转盘每等分的角度
        var width = parseInt(this.canvasDoms.canvasOut1.width / 2);
        var height = parseInt(this.canvasDoms.canvasOut1.height / 2);
        // 每一份扇形的内部绘制
        this.Items(this.itemsNum, width, height, 0, this.canvasCtxs.ctxOut1, this.text);
    };

    // 获取圆心尺寸
    lottery.prototype.getCanvasOut2 = function () {
        var width = parseInt(this.canvasDoms.canvasOut2.width / 2);
        var height = parseInt(this.canvasDoms.canvasOut2.height / 2);
        this.light(this.itemsNum, width, height, 10, this.canvasCtxs.ctxOut2);
    };

    lottery.prototype.getCanvasOut3 = function () {
        this.canvasCtxs.ctxOut3.beginPath();
        // 绘制底色为红色的圆形
        this.canvasCtxs.ctxOut3.arc(parseInt(this.canvasDoms.canvasOut3.width / 2), parseInt(this.canvasDoms.canvasOut3.height / 2),
            parseInt(this.canvasDoms.canvasOut3.width / 2), 0, 2 * Math.PI);
        this.canvasCtxs.ctxOut3.fillStyle = "#f9be34";
        this.canvasCtxs.ctxOut3.fill();
        this.canvasCtxs.ctxOut3.save();
        this.canvasCtxs.ctxOut3.restore();
    };

    lottery.prototype.getCanvasIn1 = function () {
        var width = parseInt(this.canvasDoms.canvasIn1.width / 2);
        var height = parseInt(this.canvasDoms.canvasIn1.height / 2);
        this.Items(this.itemsInNum, width, height, 40, this.canvasCtxs.ctxIn1, this.textIn);
    };

    lottery.prototype.getCanvasIn2 = function () {
        var width = parseInt(this.canvasDoms.canvasIn2.width / 2);
        var height = parseInt(this.canvasDoms.canvasIn2.height / 2);
        this.light(this.itemsInNum, width, height, 8, this.canvasCtxs.ctxIn2);
    };

    /**
     * 画中轴线
     */
    lottery.prototype.drawLine = function () {
        var width = parseInt(this.canvasDoms.canvasOut1.width / 2);
        // 画外中轴
        this.canvasCtxs.ctxPoint.beginPath();
        this.canvasCtxs.ctxPoint.moveTo(width + 16, 35);
        this.canvasCtxs.ctxPoint.lineTo(width + 32, 35);
        this.canvasCtxs.ctxPoint.lineTo(width + 24, 140);
        this.canvasCtxs.ctxPoint.fillStyle = '#ff1313';
        this.canvasCtxs.ctxPoint.closePath();
        this.canvasCtxs.ctxPoint.fill();
        // 画内中轴
        if (this.canvasDoms.canvasIn1) {
            this.canvasCtxs.ctxPoint.beginPath();
            this.canvasCtxs.ctxPoint.moveTo(width + 16, 164);
            this.canvasCtxs.ctxPoint.lineTo(width + 32, 164);
            this.canvasCtxs.ctxPoint.lineTo(width + 24, 250);
            this.canvasCtxs.ctxPoint.fillStyle = '#ff1313';
            this.canvasCtxs.ctxPoint.closePath();
            this.canvasCtxs.ctxPoint.fill();
        }
        // 保存刷新
        this.canvasCtxs.ctxPoint.save();
        this.canvasCtxs.ctxPoint.restore();
    };

    /**
     * 绘制奖品名称
     * @param {any} itemsNum  扇形数量
     * @param {any} width  圆盘宽度
     * @param {any} height  圆盘高度
     * @param {any} textRotate  文本起始角度
     * @param {any} ctx  画图容器
     * @param {any} texts  文本数组
     */
    lottery.prototype.Items = function (itemsNum, width, height, textRotate, ctx, texts) {
        let that = this;
        let itemsArc = 360 / itemsNum; // 每一分扇形的角度
        // 等分数量
        for (let i = 0; i < itemsNum; i++) {
            ctx.beginPath();
            ctx.moveTo(width, height);
            // 绘制扇形，注意下一个扇形比上一个扇形多一个itemsArc的角度
            ctx.arc(width, height, width - 5, itemsArc * i * Math.PI / 180, (itemsArc + itemsArc * i) * Math.PI / 180);
            ctx.closePath();
            // 绘制偶数扇形和奇数扇形的颜色不同
            if (i % 2 === 0) {
                ctx.fillStyle = that.color[0];
            } else {
                ctx.fillStyle = that.color[1];
            }
            ctx.fill();
            ctx.save();
        }
        for (let j = 0; j < itemsNum; j++) {
            ctx.font = "24px sans-serif";
            //ctx.fillStyle = '#FBF1A9';
            ctx.fillStyle = '#FFF';
            ctx.textAlign = 'center';
            ctx.textBaseline = 'middle';
            ctx.beginPath();
            // 将原点移至圆形圆心位置
            ctx.translate(width, height);
            // 6个的时候才有效，旋转文字，从 i+2 开始，因为扇形是从数学意义上的第四象限第一个开始的，文字目前的位置是在圆心正上方，所以起始位置要将其旋转2个扇形的角度让其与第一个扇形的位置一致。
            // ctx.rotate(itemsArc * (i + 2) * Math.PI / 180);
            ctx.rotate(itemsArc * j * Math.PI / 180);
            // 保存绘图上下文，使上一个绘制的扇形保存住。
            ctx.fillText(texts[j], textRotate, -(height * 0.8));
            ctx.restore();
        }
    };

    /**
     * 跑马灯绘制
     * @param {any} itemsNum 扇区数量
     * @param {any} width 跑马灯宽度
     * @param {any} height 跑马灯高度
     * @param {any} radius 小灯的半径
     * @param {any} ctx 画图容器
     */
    lottery.prototype.light = function (itemsNum, width, height, radius, ctx) {
        var that = this;
        that.lamp++;
        if (that.lamp >= 2) {
            that.lamp = 0;
        }
        ctx.beginPath();
        // 绘制底色为红色的圆形
        ctx.arc(width, height, width, 0, 2 * Math.PI);
        ctx.fillStyle = "#e15e34";
        ctx.fill();
        // 跑马灯小圆圈比大圆盘等分数量多一倍
        var perRate = 360 / (itemsNum * 2);
        for (let i = 0; i < itemsNum * 2; i++) {
            //var perRate = 360 / itemsNum;
            //for (let i = 0; i < itemsNum; i++) {
            ctx.save();
            ctx.beginPath();
            ctx.translate(width, height);
            ctx.rotate(perRate * i * Math.PI / 180);
            // 圆形跑马灯小圆圈
            ctx.arc(0, height - 10, radius, 0, 2 * Math.PI);
            // 跑马灯第一次闪烁时与第二次闪烁时绘制相反的颜色，再配上定时器循环闪烁就可以达到跑马灯一闪一闪的效果了
            if (that.lamp === 0) { // 第一次闪烁时偶数奇数的跑马灯各绘制一种颜色
                if (i % 2 === 0) {
                    ctx.fillStyle = "#FBF1A9";
                } else {
                    ctx.fillStyle = "#fbb936";
                }
            } else { // 第二次闪烁时偶数奇数的跑马灯颜色对调
                if (i % 2 === 0) {
                    ctx.fillStyle = "#fbb936";
                } else {
                    ctx.fillStyle = "#FBF1A9";
                }
            }
            ctx.fill();
            // 恢复之前保存的上下文，可以将循环出来的跑马灯都保存下来。没有这一句那么每循环出一个跑马灯则上一个跑马灯绘图将被覆盖，
            ctx.restore();
        }
    };
    var app = window.app;
    app.lottery = lottery;
})();
