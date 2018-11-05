<template>
<div>
    <canvas id="canvas" width='500' height='500' style="background-color:#000;"></canvas>
</div>
</template>

<script>

function Ball(){
    var me = this;
    this.x = 0;
    this.y = 0;
    this.percent = 2;
    this.radius = 50;

    this.draw = function(ctx){
        ctx.beginPath();
        ctx.strokeStyle="white";//定义线条的样式
        ctx.arc(me.x,me.y,me.radius,0, me.percent * Math.PI);
        ctx.closePath();
        
        ctx.shadowBlur=5;
        ctx.shadowOffsetX = 2;
        ctx.shadowOffsetY = 2;
        ctx.shadowColor="white";
        ctx.fillStyle="white";
        ctx.fill();
        ctx.stroke();
    }

}

export default {
    name: "test",
    components: {},
    mounted(){
        ///这个脚本中，匀速运动的原理是通过连续改变原点在x轴上的坐标，从而实现匀速运动；
        var canvas = document.getElementById('canvas');
        // Initialize the GL context
        const gl = canvas.getContext("webgl");

        // Only continue if WebGL is available and working
        if (!gl) {
            alert("Unable to initialize WebGL. Your browser or machine may not support it.");
            return;
        }

        // Set clear color to black, fully opaque
        gl.clearColor(0.0, 0.0, 0.0, 1.0);
        // Clear the color buffer with specified clear color
        gl.clear(gl.COLOR_BUFFER_BIT);
    }
};
</script>
