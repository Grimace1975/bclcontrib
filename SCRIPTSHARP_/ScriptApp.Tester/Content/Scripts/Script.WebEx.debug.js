//! Script.WebEx.debug.js
//

(function() {
function executeScript() {

Type.registerNamespace('SystemEx');

////////////////////////////////////////////////////////////////////////////////
// SystemEx.IAsyncAssetLoader

SystemEx.IAsyncAssetLoader = function() { 
};
SystemEx.IAsyncAssetLoader.prototype = {
    getAsset : null,
    pump : null,
    reset : null
}
SystemEx.IAsyncAssetLoader.registerInterface('SystemEx.IAsyncAssetLoader');


////////////////////////////////////////////////////////////////////////////////
// SystemEx.ErrorCode

SystemEx.ErrorCode = function() { 
    /// <field name="erR_FATAL" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="erR_DROP" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="erR_DISCONNECT" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="INFO" type="Number" integer="true" static="true">
    /// </field>
};
SystemEx.ErrorCode.prototype = {
    erR_FATAL: 0, 
    erR_DROP: 1, 
    erR_DISCONNECT: 2, 
    INFO: 3
}
SystemEx.ErrorCode.registerEnum('SystemEx.ErrorCode', false);


////////////////////////////////////////////////////////////////////////////////
// SystemEx.Math3D

SystemEx.Math3D = function SystemEx_Math3D() {
    /// <field name="PITCH" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="YAW" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="ROLL" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="_shortratio" type="Number" static="true">
    /// </field>
    /// <field name="_piratio" type="Number" static="true">
    /// </field>
    /// <field name="_errorHandler" type="SystemEx.ErrorHandler" static="true">
    /// </field>
    /// <field name="_m" type="Array" elementType="Array" static="true">
    /// </field>
    /// <field name="_im" type="Array" elementType="Array" static="true">
    /// </field>
    /// <field name="_tmpmat" type="Array" elementType="Array" static="true">
    /// </field>
    /// <field name="_zrot" type="Array" elementType="Array" static="true">
    /// </field>
    /// <field name="_vr" type="Array" elementType="Number" static="true">
    /// </field>
    /// <field name="_vup" type="Array" elementType="Number" static="true">
    /// </field>
    /// <field name="_vf" type="Array" elementType="Number" static="true">
    /// </field>
    /// <field name="_planE_XYZ" type="Array" elementType="Array" static="true">
    /// </field>
    /// <field name="_corners" type="Array" elementType="Array" static="true">
    /// </field>
    /// <field name="vertexNormals" type="Array" elementType="Array" static="true">
    /// </field>
}
SystemEx.Math3D.get_errorHandler = function SystemEx_Math3D$get_errorHandler() {
    /// <value type="SystemEx.ErrorHandler"></value>
    return SystemEx.Math3D._errorHandler;
}
SystemEx.Math3D.set_errorHandler = function SystemEx_Math3D$set_errorHandler(value) {
    /// <value type="SystemEx.ErrorHandler"></value>
    SystemEx.Math3D._errorHandler = value;
    return value;
}
SystemEx.Math3D.set = function SystemEx_Math3D$set(v1, v2) {
    /// <param name="v1" type="Array" elementType="Number">
    /// </param>
    /// <param name="v2" type="Array" elementType="Number">
    /// </param>
    v1[0] = v2[0];
    v1[1] = v2[1];
    v1[2] = v2[2];
}
SystemEx.Math3D.vectorSubtract = function SystemEx_Math3D$vectorSubtract(a, b, c) {
    /// <param name="a" type="Array" elementType="Number">
    /// </param>
    /// <param name="b" type="Array" elementType="Number">
    /// </param>
    /// <param name="c" type="Array" elementType="Number">
    /// </param>
    c[0] = a[0] - b[0];
    c[1] = a[1] - b[1];
    c[2] = a[2] - b[2];
}
SystemEx.Math3D.vectorSubtract2 = function SystemEx_Math3D$vectorSubtract2(a, b, c) {
    /// <param name="a" type="Array" elementType="Number" elementInteger="true">
    /// </param>
    /// <param name="b" type="Array" elementType="Number" elementInteger="true">
    /// </param>
    /// <param name="c" type="Array" elementType="Number" elementInteger="true">
    /// </param>
    c[0] = a[0] - b[0];
    c[1] = a[1] - b[1];
    c[2] = a[2] - b[2];
}
SystemEx.Math3D.vectorAdd = function SystemEx_Math3D$vectorAdd(a, b, to) {
    /// <param name="a" type="Array" elementType="Number">
    /// </param>
    /// <param name="b" type="Array" elementType="Number">
    /// </param>
    /// <param name="to" type="Array" elementType="Number">
    /// </param>
    to[0] = a[0] + b[0];
    to[1] = a[1] + b[1];
    to[2] = a[2] + b[2];
}
SystemEx.Math3D.vectorCopy = function SystemEx_Math3D$vectorCopy(from, to) {
    /// <param name="from" type="Array" elementType="Number">
    /// </param>
    /// <param name="to" type="Array" elementType="Number">
    /// </param>
    to[0] = from[0];
    to[1] = from[1];
    to[2] = from[2];
}
SystemEx.Math3D.vectorCopy2 = function SystemEx_Math3D$vectorCopy2(from, to) {
    /// <param name="from" type="Array" elementType="Number" elementInteger="true">
    /// </param>
    /// <param name="to" type="Array" elementType="Number" elementInteger="true">
    /// </param>
    to[0] = from[0];
    to[1] = from[1];
    to[2] = from[2];
}
SystemEx.Math3D.vectorCopy3 = function SystemEx_Math3D$vectorCopy3(from, to) {
    /// <param name="from" type="Array" elementType="Number" elementInteger="true">
    /// </param>
    /// <param name="to" type="Array" elementType="Number">
    /// </param>
    to[0] = from[0];
    to[1] = from[1];
    to[2] = from[2];
}
SystemEx.Math3D.vectorCopy4 = function SystemEx_Math3D$vectorCopy4(from, to) {
    /// <param name="from" type="Array" elementType="Number">
    /// </param>
    /// <param name="to" type="Array" elementType="Number" elementInteger="true">
    /// </param>
    to[0] = from[0];
    to[1] = from[1];
    to[2] = from[2];
}
SystemEx.Math3D.vectorClear = function SystemEx_Math3D$vectorClear(a) {
    /// <param name="a" type="Array" elementType="Number">
    /// </param>
    a[0] = a[1] = a[2] = 0;
}
SystemEx.Math3D.vectorEquals = function SystemEx_Math3D$vectorEquals(v1, v2) {
    /// <param name="v1" type="Array" elementType="Number">
    /// </param>
    /// <param name="v2" type="Array" elementType="Number">
    /// </param>
    /// <returns type="Boolean"></returns>
    return !(v1[0] !== v2[0] || v1[1] !== v2[1] || v1[2] !== v2[2]);
}
SystemEx.Math3D.vectorNegate = function SystemEx_Math3D$vectorNegate(from, to) {
    /// <param name="from" type="Array" elementType="Number">
    /// </param>
    /// <param name="to" type="Array" elementType="Number">
    /// </param>
    to[0] = -from[0];
    to[1] = -from[1];
    to[2] = -from[2];
}
SystemEx.Math3D.vectorSet = function SystemEx_Math3D$vectorSet(v, x, y, z) {
    /// <param name="v" type="Array" elementType="Number">
    /// </param>
    /// <param name="x" type="Number">
    /// </param>
    /// <param name="y" type="Number">
    /// </param>
    /// <param name="z" type="Number">
    /// </param>
    v[0] = x;
    v[1] = y;
    v[2] = z;
}
SystemEx.Math3D.vectorMA = function SystemEx_Math3D$vectorMA(veca, scale, vecb, to) {
    /// <param name="veca" type="Array" elementType="Number">
    /// </param>
    /// <param name="scale" type="Number">
    /// </param>
    /// <param name="vecb" type="Array" elementType="Number">
    /// </param>
    /// <param name="to" type="Array" elementType="Number">
    /// </param>
    to[0] = veca[0] + scale * vecb[0];
    to[1] = veca[1] + scale * vecb[1];
    to[2] = veca[2] + scale * vecb[2];
}
SystemEx.Math3D.vectorNormalize = function SystemEx_Math3D$vectorNormalize(v) {
    /// <param name="v" type="Array" elementType="Number">
    /// </param>
    /// <returns type="Number"></returns>
    var length = SystemEx.Math3D.vectorLength(v);
    if (length !== 0) {
        var ilength = 1 / length;
        v[0] *= ilength;
        v[1] *= ilength;
        v[2] *= ilength;
    }
    return length;
}
SystemEx.Math3D.vectorLength = function SystemEx_Math3D$vectorLength(v) {
    /// <param name="v" type="Array" elementType="Number">
    /// </param>
    /// <returns type="Number"></returns>
    return Math.sqrt(v[0] * v[0] + v[1] * v[1] + v[2] * v[2]);
}
SystemEx.Math3D.vectorInverse = function SystemEx_Math3D$vectorInverse(v) {
    /// <param name="v" type="Array" elementType="Number">
    /// </param>
    v[0] = -v[0];
    v[1] = -v[1];
    v[2] = -v[2];
}
SystemEx.Math3D.vectorScale = function SystemEx_Math3D$vectorScale(v, scale, result) {
    /// <param name="v" type="Array" elementType="Number">
    /// </param>
    /// <param name="scale" type="Number">
    /// </param>
    /// <param name="result" type="Array" elementType="Number">
    /// </param>
    result[0] = v[0] * scale;
    result[1] = v[1] * scale;
    result[2] = v[2] * scale;
}
SystemEx.Math3D.vectoyaw = function SystemEx_Math3D$vectoyaw(vec) {
    /// <param name="vec" type="Array" elementType="Number">
    /// </param>
    /// <returns type="Number"></returns>
    var yaw;
    if (vec[SystemEx.Math3D.PITCH] === 0) {
        yaw = 0;
        if (vec[SystemEx.Math3D.YAW] > 0) {
            yaw = 90;
        }
        else if (vec[SystemEx.Math3D.YAW] < 0) {
            yaw = -90;
        }
    }
    else {
        yaw = Math.truncate((Math.atan2(vec[SystemEx.Math3D.YAW], vec[SystemEx.Math3D.PITCH]) * 180 / Math.PI));
        if (yaw < 0) {
            yaw += 360;
        }
    }
    return yaw;
}
SystemEx.Math3D.vectoangles = function SystemEx_Math3D$vectoangles(value1, angles) {
    /// <param name="value1" type="Array" elementType="Number">
    /// </param>
    /// <param name="angles" type="Array" elementType="Number">
    /// </param>
    var yaw, pitch;
    if (value1[1] === 0 && value1[0] === 0) {
        yaw = 0;
        if (value1[2] > 0) {
            pitch = 90;
        }
        else {
            pitch = 270;
        }
    }
    else {
        if (value1[0] !== 0) {
            yaw = Math.truncate((Math.atan2(value1[1], value1[0]) * 180 / Math.PI));
        }
        else if (value1[1] > 0) {
            yaw = 90;
        }
        else {
            yaw = -90;
        }
        if (yaw < 0) {
            yaw += 360;
        }
        var forward = Math.sqrt(value1[0] * value1[0] + value1[1] * value1[1]);
        pitch = Math.truncate((Math.atan2(value1[2], forward) * 180 / Math.PI));
        if (pitch < 0) {
            pitch += 360;
        }
    }
    angles[SystemEx.Math3D.PITCH] = -pitch;
    angles[SystemEx.Math3D.YAW] = yaw;
    angles[SystemEx.Math3D.ROLL] = 0;
}
SystemEx.Math3D.rotatePointAroundVector = function SystemEx_Math3D$rotatePointAroundVector(dst, dir, point, degrees) {
    /// <param name="dst" type="Array" elementType="Number">
    /// </param>
    /// <param name="dir" type="Array" elementType="Number">
    /// </param>
    /// <param name="point" type="Array" elementType="Number">
    /// </param>
    /// <param name="degrees" type="Number">
    /// </param>
    SystemEx.Math3D._vf[0] = dir[0];
    SystemEx.Math3D._vf[1] = dir[1];
    SystemEx.Math3D._vf[2] = dir[2];
    SystemEx.Math3D.perpendicularVector(SystemEx.Math3D._vr, dir);
    SystemEx.Math3D.crossProduct(SystemEx.Math3D._vr, SystemEx.Math3D._vf, SystemEx.Math3D._vup);
    SystemEx.Math3D._m[0][0] = SystemEx.Math3D._vr[0];
    SystemEx.Math3D._m[1][0] = SystemEx.Math3D._vr[1];
    SystemEx.Math3D._m[2][0] = SystemEx.Math3D._vr[2];
    SystemEx.Math3D._m[0][1] = SystemEx.Math3D._vup[0];
    SystemEx.Math3D._m[1][1] = SystemEx.Math3D._vup[1];
    SystemEx.Math3D._m[2][1] = SystemEx.Math3D._vup[2];
    SystemEx.Math3D._m[0][2] = SystemEx.Math3D._vf[0];
    SystemEx.Math3D._m[1][2] = SystemEx.Math3D._vf[1];
    SystemEx.Math3D._m[2][2] = SystemEx.Math3D._vf[2];
    SystemEx.Math3D._im[0][0] = SystemEx.Math3D._m[0][0];
    SystemEx.Math3D._im[0][1] = SystemEx.Math3D._m[1][0];
    SystemEx.Math3D._im[0][2] = SystemEx.Math3D._m[2][0];
    SystemEx.Math3D._im[1][0] = SystemEx.Math3D._m[0][1];
    SystemEx.Math3D._im[1][1] = SystemEx.Math3D._m[1][1];
    SystemEx.Math3D._im[1][2] = SystemEx.Math3D._m[2][1];
    SystemEx.Math3D._im[2][0] = SystemEx.Math3D._m[0][2];
    SystemEx.Math3D._im[2][1] = SystemEx.Math3D._m[1][2];
    SystemEx.Math3D._im[2][2] = SystemEx.Math3D._m[2][2];
    SystemEx.Math3D._zrot[0][2] = SystemEx.Math3D._zrot[1][2] = SystemEx.Math3D._zrot[2][0] = SystemEx.Math3D._zrot[2][1] = 0;
    SystemEx.Math3D._zrot[2][2] = 1;
    SystemEx.Math3D._zrot[0][0] = SystemEx.Math3D._zrot[1][1] = Math.cos(SystemEx.Math3D.deG2RAD(degrees));
    SystemEx.Math3D._zrot[0][1] = Math.sin(SystemEx.Math3D.deG2RAD(degrees));
    SystemEx.Math3D._zrot[1][0] = -SystemEx.Math3D._zrot[0][1];
    SystemEx.Math3D.r_ConcatRotations(SystemEx.Math3D._m, SystemEx.Math3D._zrot, SystemEx.Math3D._tmpmat);
    SystemEx.Math3D.r_ConcatRotations(SystemEx.Math3D._tmpmat, SystemEx.Math3D._im, SystemEx.Math3D._zrot);
    for (var i = 0; i < 3; i++) {
        dst[i] = SystemEx.Math3D._zrot[i][0] * point[0] + SystemEx.Math3D._zrot[i][1] * point[1] + SystemEx.Math3D._zrot[i][2] * point[2];
    }
}
SystemEx.Math3D.makeNormalVectors = function SystemEx_Math3D$makeNormalVectors(forward, right, up) {
    /// <param name="forward" type="Array" elementType="Number">
    /// </param>
    /// <param name="right" type="Array" elementType="Number">
    /// </param>
    /// <param name="up" type="Array" elementType="Number">
    /// </param>
    right[1] = -forward[0];
    right[2] = forward[1];
    right[0] = forward[2];
    var d = SystemEx.Math3D.dotProduct(right, forward);
    SystemEx.Math3D.vectorMA(right, -d, forward, right);
    SystemEx.Math3D.vectorNormalize(right);
    SystemEx.Math3D.crossProduct(right, forward, up);
}
SystemEx.Math3D.shorT2ANGLE = function SystemEx_Math3D$shorT2ANGLE(x) {
    /// <param name="x" type="Number" integer="true">
    /// </param>
    /// <returns type="Number"></returns>
    return (x * SystemEx.Math3D._shortratio);
}
SystemEx.Math3D.r_ConcatTransforms = function SystemEx_Math3D$r_ConcatTransforms(in1, in2, result) {
    /// <param name="in1" type="Array" elementType="Array">
    /// </param>
    /// <param name="in2" type="Array" elementType="Array">
    /// </param>
    /// <param name="result" type="Array" elementType="Array">
    /// </param>
    result[0][0] = in1[0][0] * in2[0][0] + in1[0][1] * in2[1][0] + in1[0][2] * in2[2][0];
    result[0][1] = in1[0][0] * in2[0][1] + in1[0][1] * in2[1][1] + in1[0][2] * in2[2][1];
    result[0][2] = in1[0][0] * in2[0][2] + in1[0][1] * in2[1][2] + in1[0][2] * in2[2][2];
    result[0][3] = in1[0][0] * in2[0][3] + in1[0][1] * in2[1][3] + in1[0][2] * in2[2][3] + in1[0][3];
    result[1][0] = in1[1][0] * in2[0][0] + in1[1][1] * in2[1][0] + in1[1][2] * in2[2][0];
    result[1][1] = in1[1][0] * in2[0][1] + in1[1][1] * in2[1][1] + in1[1][2] * in2[2][1];
    result[1][2] = in1[1][0] * in2[0][2] + in1[1][1] * in2[1][2] + in1[1][2] * in2[2][2];
    result[1][3] = in1[1][0] * in2[0][3] + in1[1][1] * in2[1][3] + in1[1][2] * in2[2][3] + in1[1][3];
    result[2][0] = in1[2][0] * in2[0][0] + in1[2][1] * in2[1][0] + in1[2][2] * in2[2][0];
    result[2][1] = in1[2][0] * in2[0][1] + in1[2][1] * in2[1][1] + in1[2][2] * in2[2][1];
    result[2][2] = in1[2][0] * in2[0][2] + in1[2][1] * in2[1][2] + in1[2][2] * in2[2][2];
    result[2][3] = in1[2][0] * in2[0][3] + in1[2][1] * in2[1][3] + in1[2][2] * in2[2][3] + in1[2][3];
}
SystemEx.Math3D.r_ConcatRotations = function SystemEx_Math3D$r_ConcatRotations(in1, in2, result) {
    /// <param name="in1" type="Array" elementType="Array">
    /// </param>
    /// <param name="in2" type="Array" elementType="Array">
    /// </param>
    /// <param name="result" type="Array" elementType="Array">
    /// </param>
    result[0][0] = in1[0][0] * in2[0][0] + in1[0][1] * in2[1][0] + in1[0][2] * in2[2][0];
    result[0][1] = in1[0][0] * in2[0][1] + in1[0][1] * in2[1][1] + in1[0][2] * in2[2][1];
    result[0][2] = in1[0][0] * in2[0][2] + in1[0][1] * in2[1][2] + in1[0][2] * in2[2][2];
    result[1][0] = in1[1][0] * in2[0][0] + in1[1][1] * in2[1][0] + in1[1][2] * in2[2][0];
    result[1][1] = in1[1][0] * in2[0][1] + in1[1][1] * in2[1][1] + in1[1][2] * in2[2][1];
    result[1][2] = in1[1][0] * in2[0][2] + in1[1][1] * in2[1][2] + in1[1][2] * in2[2][2];
    result[2][0] = in1[2][0] * in2[0][0] + in1[2][1] * in2[1][0] + in1[2][2] * in2[2][0];
    result[2][1] = in1[2][0] * in2[0][1] + in1[2][1] * in2[1][1] + in1[2][2] * in2[2][1];
    result[2][2] = in1[2][0] * in2[0][2] + in1[2][1] * in2[1][2] + in1[2][2] * in2[2][2];
}
SystemEx.Math3D.projectPointOnPlane = function SystemEx_Math3D$projectPointOnPlane(dst, p, normal) {
    /// <param name="dst" type="Array" elementType="Number">
    /// </param>
    /// <param name="p" type="Array" elementType="Number">
    /// </param>
    /// <param name="normal" type="Array" elementType="Number">
    /// </param>
    var inv_denom = 1 / SystemEx.Math3D.dotProduct(normal, normal);
    var d = SystemEx.Math3D.dotProduct(normal, p) * inv_denom;
    dst[0] = normal[0] * inv_denom;
    dst[1] = normal[1] * inv_denom;
    dst[2] = normal[2] * inv_denom;
    dst[0] = p[0] - d * dst[0];
    dst[1] = p[1] - d * dst[1];
    dst[2] = p[2] - d * dst[2];
}
SystemEx.Math3D.perpendicularVector = function SystemEx_Math3D$perpendicularVector(dst, src) {
    /// <param name="dst" type="Array" elementType="Number">
    /// </param>
    /// <param name="src" type="Array" elementType="Number">
    /// </param>
    var pos;
    var i;
    var minelem = 1;
    for (pos = 0, i = 0; i < 3; i++) {
        if (Math.abs(src[i]) < minelem) {
            pos = i;
            minelem = Math.abs(src[i]);
        }
    }
    SystemEx.Math3D.projectPointOnPlane(dst, SystemEx.Math3D._planE_XYZ[pos], src);
    SystemEx.Math3D.vectorNormalize(dst);
}
SystemEx.Math3D.boxOnPlaneSide = function SystemEx_Math3D$boxOnPlaneSide(emins, emaxs, p) {
    /// <param name="emins" type="Array" elementType="Number">
    /// </param>
    /// <param name="emaxs" type="Array" elementType="Number">
    /// </param>
    /// <param name="p" type="SystemEx.Plane3">
    /// </param>
    /// <returns type="Number" integer="true"></returns>
    var dist1, dist2;
    var sides;
    if (p.type < 3) {
        if (p.dist <= emins[p.type]) {
            return 1;
        }
        if (p.dist >= emaxs[p.type]) {
            return 2;
        }
        return 3;
    }
    switch (p.signbits) {
        case 0:
            dist1 = p.normal[0] * emaxs[0] + p.normal[1] * emaxs[1] + p.normal[2] * emaxs[2];
            dist2 = p.normal[0] * emins[0] + p.normal[1] * emins[1] + p.normal[2] * emins[2];
            break;
        case 1:
            dist1 = p.normal[0] * emins[0] + p.normal[1] * emaxs[1] + p.normal[2] * emaxs[2];
            dist2 = p.normal[0] * emaxs[0] + p.normal[1] * emins[1] + p.normal[2] * emins[2];
            break;
        case 2:
            dist1 = p.normal[0] * emaxs[0] + p.normal[1] * emins[1] + p.normal[2] * emaxs[2];
            dist2 = p.normal[0] * emins[0] + p.normal[1] * emaxs[1] + p.normal[2] * emins[2];
            break;
        case 3:
            dist1 = p.normal[0] * emins[0] + p.normal[1] * emins[1] + p.normal[2] * emaxs[2];
            dist2 = p.normal[0] * emaxs[0] + p.normal[1] * emaxs[1] + p.normal[2] * emins[2];
            break;
        case 4:
            dist1 = p.normal[0] * emaxs[0] + p.normal[1] * emaxs[1] + p.normal[2] * emins[2];
            dist2 = p.normal[0] * emins[0] + p.normal[1] * emins[1] + p.normal[2] * emaxs[2];
            break;
        case 5:
            dist1 = p.normal[0] * emins[0] + p.normal[1] * emaxs[1] + p.normal[2] * emins[2];
            dist2 = p.normal[0] * emaxs[0] + p.normal[1] * emins[1] + p.normal[2] * emaxs[2];
            break;
        case 6:
            dist1 = p.normal[0] * emaxs[0] + p.normal[1] * emins[1] + p.normal[2] * emins[2];
            dist2 = p.normal[0] * emins[0] + p.normal[1] * emaxs[1] + p.normal[2] * emaxs[2];
            break;
        case 7:
            dist1 = p.normal[0] * emins[0] + p.normal[1] * emins[1] + p.normal[2] * emins[2];
            dist2 = p.normal[0] * emaxs[0] + p.normal[1] * emaxs[1] + p.normal[2] * emaxs[2];
            break;
        default:
            dist1 = dist2 = 0;
            break;
    }
    sides = 0;
    if (dist1 >= p.dist) {
        sides = 1;
    }
    if (dist2 < p.dist) {
        sides |= 2;
    }
    return sides;
}
SystemEx.Math3D.boxOnPlaneSide2 = function SystemEx_Math3D$boxOnPlaneSide2(emins, emaxs, p) {
    /// <param name="emins" type="Array" elementType="Number">
    /// </param>
    /// <param name="emaxs" type="Array" elementType="Number">
    /// </param>
    /// <param name="p" type="SystemEx.Plane3">
    /// </param>
    /// <returns type="Number" integer="true"></returns>
    for (var i = 0; i < 3; i++) {
        if (p.normal[i] < 0) {
            SystemEx.Math3D._corners[0][i] = emins[i];
            SystemEx.Math3D._corners[1][i] = emaxs[i];
        }
        else {
            SystemEx.Math3D._corners[1][i] = emins[i];
            SystemEx.Math3D._corners[0][i] = emaxs[i];
        }
    }
    var dist1 = SystemEx.Math3D.dotProduct(p.normal, SystemEx.Math3D._corners[0]) - p.dist;
    var dist2 = SystemEx.Math3D.dotProduct(p.normal, SystemEx.Math3D._corners[1]) - p.dist;
    var sides = 0;
    if (dist1 >= 0) {
        sides = 1;
    }
    if (dist2 < 0) {
        sides |= 2;
    }
    return sides;
}
SystemEx.Math3D.angleVectors = function SystemEx_Math3D$angleVectors(angles, forward, right, up) {
    /// <param name="angles" type="Array" elementType="Number">
    /// </param>
    /// <param name="forward" type="Array" elementType="Number">
    /// </param>
    /// <param name="right" type="Array" elementType="Number">
    /// </param>
    /// <param name="up" type="Array" elementType="Number">
    /// </param>
    var cr = 2 * SystemEx.Math3D._piratio;
    var angle = (angles[SystemEx.Math3D.YAW] * cr);
    var sy = Math.sin(angle);
    var cy = Math.cos(angle);
    angle = (angles[SystemEx.Math3D.PITCH] * cr);
    var sp = Math.sin(angle);
    var cp = Math.cos(angle);
    if (forward != null) {
        forward[0] = cp * cy;
        forward[1] = cp * sy;
        forward[2] = -sp;
    }
    if (right != null || up != null) {
        angle = (angles[SystemEx.Math3D.ROLL] * cr);
        var sr = Math.sin(angle);
        cr = Math.cos(angle);
        if (right != null) {
            right[0] = (-sr * sp * cy + cr * sy);
            right[1] = (-sr * sp * sy + -cr * cy);
            right[2] = -sr * cp;
        }
        if (up != null) {
            up[0] = (cr * sp * cy + sr * sy);
            up[1] = (cr * sp * sy + -sr * cy);
            up[2] = cr * cp;
        }
    }
}
SystemEx.Math3D.g_ProjectSource = function SystemEx_Math3D$g_ProjectSource(point, distance, forward, right, result) {
    /// <param name="point" type="Array" elementType="Number">
    /// </param>
    /// <param name="distance" type="Array" elementType="Number">
    /// </param>
    /// <param name="forward" type="Array" elementType="Number">
    /// </param>
    /// <param name="right" type="Array" elementType="Number">
    /// </param>
    /// <param name="result" type="Array" elementType="Number">
    /// </param>
    result[0] = point[0] + forward[0] * distance[0] + right[0] * distance[1];
    result[1] = point[1] + forward[1] * distance[0] + right[1] * distance[1];
    result[2] = point[2] + forward[2] * distance[0] + right[2] * distance[1] + distance[2];
}
SystemEx.Math3D.dotProduct = function SystemEx_Math3D$dotProduct(x, y) {
    /// <param name="x" type="Array" elementType="Number">
    /// </param>
    /// <param name="y" type="Array" elementType="Number">
    /// </param>
    /// <returns type="Number"></returns>
    return x[0] * y[0] + x[1] * y[1] + x[2] * y[2];
}
SystemEx.Math3D.crossProduct = function SystemEx_Math3D$crossProduct(v1, v2, cross) {
    /// <param name="v1" type="Array" elementType="Number">
    /// </param>
    /// <param name="v2" type="Array" elementType="Number">
    /// </param>
    /// <param name="cross" type="Array" elementType="Number">
    /// </param>
    cross[0] = v1[1] * v2[2] - v1[2] * v2[1];
    cross[1] = v1[2] * v2[0] - v1[0] * v2[2];
    cross[2] = v1[0] * v2[1] - v1[1] * v2[0];
}
SystemEx.Math3D.q_log2 = function SystemEx_Math3D$q_log2(val) {
    /// <param name="val" type="Number" integer="true">
    /// </param>
    /// <returns type="Number" integer="true"></returns>
    var answer = 0;
    while ((val >>= 1) > 0) {
        answer++;
    }
    return answer;
}
SystemEx.Math3D.deG2RAD = function SystemEx_Math3D$deG2RAD(v) {
    /// <param name="v" type="Number">
    /// </param>
    /// <returns type="Number"></returns>
    return (v * Math.PI) / 180;
}
SystemEx.Math3D.anglemod = function SystemEx_Math3D$anglemod(a) {
    /// <param name="a" type="Number">
    /// </param>
    /// <returns type="Number"></returns>
    return SystemEx.Math3D._shortratio * (Math.truncate((a / SystemEx.Math3D._shortratio)) & 65535);
}
SystemEx.Math3D.anglE2SHORT = function SystemEx_Math3D$anglE2SHORT(x) {
    /// <param name="x" type="Number">
    /// </param>
    /// <returns type="Number" integer="true"></returns>
    return ((x / SystemEx.Math3D._shortratio) & 65535);
}
SystemEx.Math3D.lerpAngle = function SystemEx_Math3D$lerpAngle(a2, a1, frac) {
    /// <param name="a2" type="Number">
    /// </param>
    /// <param name="a1" type="Number">
    /// </param>
    /// <param name="frac" type="Number">
    /// </param>
    /// <returns type="Number"></returns>
    if (a1 - a2 > 180) {
        a1 -= 360;
    }
    if (a1 - a2 < -180) {
        a1 += 360;
    }
    return a2 + frac * (a1 - a2);
}
SystemEx.Math3D.calcFov = function SystemEx_Math3D$calcFov(fov_x, width, height) {
    /// <param name="fov_x" type="Number">
    /// </param>
    /// <param name="width" type="Number">
    /// </param>
    /// <param name="height" type="Number">
    /// </param>
    /// <returns type="Number"></returns>
    var a = 0;
    var x;
    if (((fov_x < 1) || (fov_x > 179)) && (SystemEx.Math3D._errorHandler != null)) {
        SystemEx.Math3D._errorHandler.invoke(SystemEx.ErrorCode.erR_DROP, 'Bad fov: ' + fov_x, null);
    }
    x = width / Math.tan(fov_x * SystemEx.Math3D._piratio);
    a = Math.atan(height / x);
    a = a / SystemEx.Math3D._piratio;
    return a;
}


////////////////////////////////////////////////////////////////////////////////
// SystemEx.Plane3

SystemEx.Plane3 = function SystemEx_Plane3() {
    /// <field name="normal" type="Array" elementType="Number">
    /// </field>
    /// <field name="dist" type="Number">
    /// </field>
    /// <field name="type" type="Number" integer="true">
    /// </field>
    /// <field name="signbits" type="Number" integer="true">
    /// </field>
    /// <field name="pad" type="Array" elementType="Number" elementInteger="true">
    /// </field>
    this.normal = new Array(3);
    this.pad = [ 0, 0 ];
}
SystemEx.Plane3.prototype = {
    dist: 0,
    type: 0,
    signbits: 0,
    
    set: function SystemEx_Plane3$set(c) {
        /// <param name="c" type="SystemEx.Plane3">
        /// </param>
        SystemEx.Math3D.set(this.normal, c.normal);
        this.dist = c.dist;
        this.type = c.type;
        this.signbits = c.signbits;
        this.pad[0] = c.pad[0];
        this.pad[1] = c.pad[1];
    },
    
    clear: function SystemEx_Plane3$clear() {
        SystemEx.Math3D.vectorClear(this.normal);
        this.dist = 0;
        this.type = 0;
        this.signbits = 0;
        this.pad[0] = 0;
        this.pad[1] = 0;
    }
}


////////////////////////////////////////////////////////////////////////////////
// SystemEx.ByteBuilder

SystemEx.ByteBuilder = function SystemEx_ByteBuilder(data, length) {
    /// <summary>
    /// sizebuf_t
    /// </summary>
    /// <param name="data" type="Array" elementType="Number" elementInteger="true">
    /// </param>
    /// <param name="length" type="Number" integer="true">
    /// </param>
    /// <field name="_stringBuffer" type="Array" elementType="Number" elementInteger="true" static="true">
    /// </field>
    /// <field name="canOverflow" type="Boolean">
    /// </field>
    /// <field name="hasOverflowed" type="Boolean">
    /// </field>
    /// <field name="data" type="Array" elementType="Number" elementInteger="true">
    /// </field>
    /// <field name="maxCapacity" type="Number" integer="true">
    /// </field>
    /// <field name="length" type="Number" integer="true">
    /// </field>
    /// <field name="readIndex" type="Number" integer="true">
    /// </field>
    /// <field name="_errorHandler" type="SystemEx.ErrorHandler" static="true">
    /// </field>
    this.data = data;
    this.maxCapacity = length;
}
SystemEx.ByteBuilder.get_errorHandler = function SystemEx_ByteBuilder$get_errorHandler() {
    /// <value type="SystemEx.ErrorHandler"></value>
    return SystemEx.ByteBuilder._errorHandler;
}
SystemEx.ByteBuilder.set_errorHandler = function SystemEx_ByteBuilder$set_errorHandler(value) {
    /// <value type="SystemEx.ErrorHandler"></value>
    SystemEx.ByteBuilder._errorHandler = value;
    return value;
}
SystemEx.ByteBuilder.prototype = {
    
    writeCoord: function SystemEx_ByteBuilder$writeCoord(f) {
        /// <param name="f" type="Number">
        /// </param>
        this.writeInt16((f * 8));
    },
    
    writePos: function SystemEx_ByteBuilder$writePos(pos) {
        /// <param name="pos" type="Array" elementType="Number">
        /// </param>
        this.writeInt16((pos[0] * 8));
        this.writeInt16((pos[1] * 8));
        this.writeInt16((pos[2] * 8));
    },
    
    writeAngle: function SystemEx_ByteBuilder$writeAngle(f) {
        /// <param name="f" type="Number">
        /// </param>
        this.writeByte((f * 256 / 360));
    },
    
    writeAngle16: function SystemEx_ByteBuilder$writeAngle16(f) {
        /// <param name="f" type="Number">
        /// </param>
        this.writeInt16(SystemEx.Math3D.anglE2SHORT(f));
    },
    
    writeDir: function SystemEx_ByteBuilder$writeDir(dir) {
        /// <param name="dir" type="Array" elementType="Number">
        /// </param>
        if (dir == null) {
            this.writeByte(0);
            return;
        }
        var bestd = 0;
        var best = 0;
        for (var index = 0; index < SystemEx.Math3D.vertexNormals.length; index++) {
            var d = SystemEx.Math3D.dotProduct(dir, SystemEx.Math3D.vertexNormals[index]);
            if (d > bestd) {
                bestd = d;
                best = index;
            }
        }
        this.writeByte(best);
    },
    
    readDir: function SystemEx_ByteBuilder$readDir(dir) {
        /// <param name="dir" type="Array" elementType="Number">
        /// </param>
        var value = this.readByte();
        if ((value >= SystemEx.Math3D.vertexNormals.length) && (SystemEx.ByteBuilder._errorHandler != null)) {
            SystemEx.ByteBuilder._errorHandler.invoke(SystemEx.ErrorCode.erR_DROP, 'MSF_ReadDir: out of range', null);
        }
        SystemEx.Math3D.vectorCopy(SystemEx.Math3D.vertexNormals[value], dir);
    },
    
    readCoord: function SystemEx_ByteBuilder$readCoord() {
        /// <returns type="Number"></returns>
        return this.readInt16() * (1 / 8);
    },
    
    readPos: function SystemEx_ByteBuilder$readPos(pos) {
        /// <param name="pos" type="Array" elementType="Number">
        /// </param>
        pos[0] = this.readInt16() * (1 / 8);
        pos[1] = this.readInt16() * (1 / 8);
        pos[2] = this.readInt16() * (1 / 8);
    },
    
    readAngle: function SystemEx_ByteBuilder$readAngle() {
        /// <returns type="Number"></returns>
        return this.readChar() * (360 / 256);
    },
    
    readAngle16: function SystemEx_ByteBuilder$readAngle16() {
        /// <returns type="Number"></returns>
        return SystemEx.Math3D.shorT2ANGLE(this.readInt16());
    },
    
    writeChar: function SystemEx_ByteBuilder$writeChar(c) {
        /// <param name="c" type="String">
        /// </param>
        this.data[this.getSpace(1)] = (c & 255);
    },
    
    writeByte: function SystemEx_ByteBuilder$writeByte(c) {
        /// <param name="c" type="Number" integer="true">
        /// </param>
        this.data[this.getSpace(1)] = (c & 255);
    },
    
    writeInt16: function SystemEx_ByteBuilder$writeInt16(c) {
        /// <param name="c" type="Number" integer="true">
        /// </param>
        var index = this.getSpace(2);
        this.data[index++] = (c & 255);
        this.data[index] = ((c >> 8) & 255);
    },
    
    writeInt32: function SystemEx_ByteBuilder$writeInt32(c) {
        /// <param name="c" type="Number" integer="true">
        /// </param>
        var index = this.getSpace(4);
        this.data[index++] = (c & 255);
        this.data[index++] = ((c >> 8) & 255);
        this.data[index++] = ((c >> 16) & 255);
        this.data[index++] = ((c >> 24) & 255);
    },
    
    writeInt64: function SystemEx_ByteBuilder$writeInt64(c) {
        /// <param name="c" type="Number" integer="true">
        /// </param>
        this.writeInt32(c);
    },
    
    writeSingle: function SystemEx_ByteBuilder$writeSingle(f) {
        /// <param name="f" type="Number">
        /// </param>
        this.writeInt32(SystemEx.JSConvert.singleToInt32Bits(f));
    },
    
    writeString: function SystemEx_ByteBuilder$writeString(s) {
        /// <param name="s" type="String">
        /// </param>
        if (s == null) {
            s = String.Empty;
        }
        this.append(SystemEx.JSConvert.stringToBytes(s));
        this.writeByte(0);
    },
    
    beginReading: function SystemEx_ByteBuilder$beginReading() {
        this.readIndex = 0;
    },
    
    readChar: function SystemEx_ByteBuilder$readChar() {
        /// <returns type="String"></returns>
        var c;
        if (this.readIndex + 1 > this.length) {
            c = 255;
        }
        else {
            c = this.data[this.readIndex];
        }
        this.readIndex++;
        return c;
    },
    
    readByte: function SystemEx_ByteBuilder$readByte() {
        /// <returns type="Number" integer="true"></returns>
        var c;
        if (this.readIndex + 1 > this.length) {
            c = 255;
        }
        else {
            c = (this.data[this.readIndex] & 255);
        }
        this.readIndex++;
        return c;
    },
    
    readInt16: function SystemEx_ByteBuilder$readInt16() {
        /// <returns type="Number" integer="true"></returns>
        var c;
        if (this.readIndex + 2 > this.length) {
            c = -1;
        }
        else {
            c = ((this.data[this.readIndex] & 255) + (this.data[this.readIndex + 1] << 8));
        }
        this.readIndex += 2;
        return c;
    },
    
    readInt32: function SystemEx_ByteBuilder$readInt32() {
        /// <returns type="Number" integer="true"></returns>
        var c;
        if (this.readIndex + 4 > this.length) {
            if (SystemEx.ByteBuilder._errorHandler != null) {
                SystemEx.ByteBuilder._errorHandler.invoke(SystemEx.ErrorCode.INFO, 'buffer underrun in ReadLong!', null);
            }
            c = -1;
        }
        else {
            c = (this.data[this.readIndex] & 255) | ((this.data[this.readIndex + 1] & 255) << 8) | ((this.data[this.readIndex + 2] & 255) << 16) | ((this.data[this.readIndex + 3] & 255) << 24);
        }
        this.readIndex += 4;
        return c;
    },
    
    readInt64: function SystemEx_ByteBuilder$readInt64() {
        /// <returns type="Number" integer="true"></returns>
        return this.readInt32();
    },
    
    readSingle: function SystemEx_ByteBuilder$readSingle() {
        /// <returns type="Number"></returns>
        return SystemEx.JSConvert.int32BitsToSingle(this.readInt32());
    },
    
    readString: function SystemEx_ByteBuilder$readString() {
        /// <returns type="String"></returns>
        var index = 0;
        do {
            var c = this.readByte();
            if ((c === 255) || (c === 0)) {
                break;
            }
            SystemEx.ByteBuilder._stringBuffer[index] = c;
            index++;
        } while (index < 2047);
        var ret = SystemEx.JSConvert.bytesToString(SystemEx.ByteBuilder._stringBuffer, 0, index);
        return ret;
    },
    
    readStringLine: function SystemEx_ByteBuilder$readStringLine() {
        /// <returns type="String"></returns>
        var index = 0;
        do {
            var c = this.readByte();
            if ((c === 255) || (c === 0) || (c === 10)) {
                break;
            }
            SystemEx.ByteBuilder._stringBuffer[index] = c;
            index++;
        } while (index < 2047);
        var ret = SystemEx.JSConvert.bytesToString(SystemEx.ByteBuilder._stringBuffer, 0, index).trim();
        return ret;
    },
    
    readData: function SystemEx_ByteBuilder$readData(data, length) {
        /// <param name="data" type="Array" elementType="Number" elementInteger="true">
        /// </param>
        /// <param name="length" type="Number" integer="true">
        /// </param>
        for (var index = 0; index < length; index++) {
            data[index] = this.readByte();
        }
    },
    
    canOverflow: false,
    hasOverflowed: false,
    data: null,
    maxCapacity: 0,
    length: 0,
    readIndex: 0,
    
    clear: function SystemEx_ByteBuilder$clear() {
        if (this.data != null) {
            SystemEx.JSArrayEx.clear(this.data, 0, this.data.length);
        }
        this.length = 0;
        this.hasOverflowed = false;
    },
    
    getSpace: function SystemEx_ByteBuilder$getSpace(length) {
        /// <param name="length" type="Number" integer="true">
        /// </param>
        /// <returns type="Number" integer="true"></returns>
        if (this.length + length > this.maxCapacity) {
            if (SystemEx.ByteBuilder._errorHandler != null) {
                if (!this.canOverflow) {
                    SystemEx.ByteBuilder._errorHandler.invoke(SystemEx.ErrorCode.erR_FATAL, 'SZ_GetSpace: overflow without allowoverflow set', null);
                }
                if (length > this.maxCapacity) {
                    SystemEx.ByteBuilder._errorHandler.invoke(SystemEx.ErrorCode.erR_FATAL, 'SZ_GetSpace: ' + length + ' is > full buffer size', null);
                }
                SystemEx.ByteBuilder._errorHandler.invoke(SystemEx.ErrorCode.INFO, 'SZ_GetSpace: overflow\n', null);
            }
            this.clear();
            this.hasOverflowed = true;
        }
        var lastLength = this.length;
        this.length += length;
        return lastLength;
    },
    
    append: function SystemEx_ByteBuilder$append(data) {
        /// <param name="data" type="Array" elementType="Number" elementInteger="true">
        /// </param>
        var length = data.length;
        SystemEx.JSArrayEx.copy(data, 0, data, this.getSpace(length), length);
    },
    
    append2: function SystemEx_ByteBuilder$append2(data, length) {
        /// <param name="data" type="Array" elementType="Number" elementInteger="true">
        /// </param>
        /// <param name="length" type="Number" integer="true">
        /// </param>
        SystemEx.JSArrayEx.copy(data, 0, data, this.getSpace(length), length);
    },
    
    append3: function SystemEx_ByteBuilder$append3(data, offset, length) {
        /// <param name="data" type="Array" elementType="Number" elementInteger="true">
        /// </param>
        /// <param name="offset" type="Number" integer="true">
        /// </param>
        /// <param name="length" type="Number" integer="true">
        /// </param>
        SystemEx.JSArrayEx.copy(data, offset, data, this.getSpace(length), length);
    },
    
    print: function SystemEx_ByteBuilder$print(data2) {
        /// <param name="data2" type="String">
        /// </param>
        var length = data2.length;
        var str = SystemEx.JSConvert.stringToBytes(data2);
        if (this.length !== 0) {
            if (this.data[this.length - 1] !== 0) {
                SystemEx.JSArrayEx.copy(str, 0, this.data, this.getSpace(length + 1), length);
            }
            else {
                SystemEx.JSArrayEx.copy(str, 0, this.data, this.getSpace(length) - 1, length);
            }
        }
        else {
            SystemEx.JSArrayEx.copy(str, 0, this.data, this.getSpace(length), length);
        }
        this.data[this.length - 1] = 0;
    }
}


////////////////////////////////////////////////////////////////////////////////
// SystemEx.JSSystem

SystemEx.JSSystem = function SystemEx_JSSystem() {
}
SystemEx.JSSystem.tryCatch = function SystemEx_JSSystem$tryCatch(e, name) {
    /// <param name="e" type="Error">
    /// </param>
    /// <param name="name" type="String">
    /// </param>
    /// <returns type="Boolean"></returns>
    console.warn(e.message);
    throw e;
}
SystemEx.JSSystem.get_currentMSecond = function SystemEx_JSSystem$get_currentMSecond() {
    /// <value type="Number" integer="true"></value>
    return (new Date()).getTime();
}


////////////////////////////////////////////////////////////////////////////////
// SystemEx.JSString

SystemEx.JSString = function SystemEx_JSString() {
}
SystemEx.JSString.charsToString = function SystemEx_JSString$charsToString(b, startIndex, length) {
    /// <param name="b" type="Array" elementType="String">
    /// </param>
    /// <param name="startIndex" type="Number" integer="true">
    /// </param>
    /// <param name="length" type="Number" integer="true">
    /// </param>
    /// <returns type="String"></returns>
    if (length === 0) {
        length = b.length;
    }
    var chars = new Array(length);
    for (var index = 0; index < length; index++) {
        chars[index] = b[startIndex + index];
    }
    return String.fromCharCode(chars);
}
SystemEx.JSString.stringToChars = function SystemEx_JSString$stringToChars(s) {
    /// <param name="s" type="String">
    /// </param>
    /// <returns type="Array" elementType="String"></returns>
    var b = new Array(s.length);
    for (var index = 0; index < s.length; index++) {
        b[index] = s.charCodeAt(index);
    }
    return b;
}
SystemEx.JSString.equals = function SystemEx_JSString$equals(s1, s2, ignoreCase) {
    /// <param name="s1" type="String">
    /// </param>
    /// <param name="s2" type="String">
    /// </param>
    /// <param name="ignoreCase" type="Boolean">
    /// </param>
    /// <returns type="Boolean"></returns>
    return String.equals(s1, s2, ignoreCase);
}


////////////////////////////////////////////////////////////////////////////////
// SystemEx.JSArrayEx

SystemEx.JSArrayEx = function SystemEx_JSArrayEx() {
}
SystemEx.JSArrayEx.clear = function SystemEx_JSArrayEx$clear(array, index, length) {
    /// <param name="array" type="Array">
    /// </param>
    /// <param name="index" type="Number" integer="true">
    /// </param>
    /// <param name="length" type="Number" integer="true">
    /// </param>
}
SystemEx.JSArrayEx.copy = function SystemEx_JSArrayEx$copy(source, sourceIndex, destination, destinationIndex, length) {
    /// <param name="source" type="Array">
    /// </param>
    /// <param name="sourceIndex" type="Number" integer="true">
    /// </param>
    /// <param name="destination" type="Array">
    /// </param>
    /// <param name="destinationIndex" type="Number" integer="true">
    /// </param>
    /// <param name="length" type="Number" integer="true">
    /// </param>
    if ((source == null) || (destination == null)) {
        throw new Error('NullPointerException:');
    }
    var sourceLength = source.length;
    var destinationLength = destination.length;
    if ((sourceIndex < 0) || (destinationIndex < 0) || (length < 0) || (sourceIndex + length > sourceLength) || (destinationIndex + length > destinationLength)) {
        throw new Error('IndexOutOfBoundsException:');
    }
    SystemEx.JSArrayEx._internalNativeCopy(source, sourceIndex, destination, destinationIndex, length);
}
SystemEx.JSArrayEx._internalNativeCopy = function SystemEx_JSArrayEx$_internalNativeCopy(source, sourceIndex, destination, destinationIndex, length) {
    /// <param name="source" type="Array" elementType="Object">
    /// </param>
    /// <param name="sourceIndex" type="Number" integer="true">
    /// </param>
    /// <param name="destination" type="Array" elementType="Object">
    /// </param>
    /// <param name="destinationIndex" type="Number" integer="true">
    /// </param>
    /// <param name="length" type="Number" integer="true">
    /// </param>
    if ((source === destination) && (sourceIndex < destinationIndex)) {
        sourceIndex += length;
        for (var index = destinationIndex + length; index-- > destinationIndex; ) {
            destination[index] = source[--sourceIndex];
        }
    }
    else {
        for (var index = destinationIndex + length; destinationIndex < index; ) {
            destination[destinationIndex++] = source[sourceIndex++];
        }
    }
}


////////////////////////////////////////////////////////////////////////////////
// SystemEx.JSConvert

SystemEx.JSConvert = function SystemEx_JSConvert() {
    /// <field name="short_MinValue" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="int_MinValue" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="long_MinValue" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="_wba" type="Int8Array" static="true">
    /// </field>
    /// <field name="_wia" type="Int32Array" static="true">
    /// </field>
    /// <field name="_wfa" type="Float32Array" static="true">
    /// </field>
}
SystemEx.JSConvert.char_IsDigit = function SystemEx_JSConvert$char_IsDigit(x) {
    /// <param name="x" type="String">
    /// </param>
    /// <returns type="Boolean"></returns>
    var y = parseInt(x);
    return ((isNaN(y)) ? false : ((x === y) && (x.toString() === y.toString())));
}
SystemEx.JSConvert.singleToInt32Bits = function SystemEx_JSConvert$singleToInt32Bits(v) {
    /// <param name="v" type="Number">
    /// </param>
    /// <returns type="Number" integer="true"></returns>
    SystemEx.JSConvert._wfa[0] = v;
    return SystemEx.JSConvert._wia[0];
}
SystemEx.JSConvert.int32BitsToSingle = function SystemEx_JSConvert$int32BitsToSingle(v) {
    /// <param name="v" type="Number" integer="true">
    /// </param>
    /// <returns type="Number"></returns>
    SystemEx.JSConvert._wia[0] = v;
    return SystemEx.JSConvert._wfa[0];
}
SystemEx.JSConvert.bytesToString = function SystemEx_JSConvert$bytesToString(b, offset, length) {
    /// <param name="b" type="Array" elementType="Number" elementInteger="true">
    /// </param>
    /// <param name="offset" type="Number" integer="true">
    /// </param>
    /// <param name="length" type="Number" integer="true">
    /// </param>
    /// <returns type="String"></returns>
    if (length === 0) {
        length = b.length;
    }
    var chars = new Array(length);
    for (var index = 0; index < length; index++) {
        chars[index] = b[offset + index];
    }
    return String.fromCharCode(chars);
}
SystemEx.JSConvert.stringToBytes = function SystemEx_JSConvert$stringToBytes(s, b, offset) {
    /// <param name="s" type="String">
    /// </param>
    /// <param name="b" type="Array" elementType="Number" elementInteger="true">
    /// </param>
    /// <param name="offset" type="Number" integer="true">
    /// </param>
    /// <returns type="Array" elementType="Number" elementInteger="true"></returns>
    if (b == null) {
        b = new Array(s.length);
    }
    for (var index = 0; index < s.length; index++) {
        b[offset + index] = s.charCodeAt(index);
    }
    return b;
}
SystemEx.JSConvert.int16ToString = function SystemEx_JSConvert$int16ToString(value, toBase) {
    /// <param name="value" type="Number" integer="true">
    /// </param>
    /// <param name="toBase" type="Number" integer="true">
    /// </param>
    /// <returns type="String"></returns>
    return value.toString();
}
SystemEx.JSConvert.int32ToString = function SystemEx_JSConvert$int32ToString(value, toBase) {
    /// <param name="value" type="Number" integer="true">
    /// </param>
    /// <param name="toBase" type="Number" integer="true">
    /// </param>
    /// <returns type="String"></returns>
    return value.toString();
}
SystemEx.JSConvert.int64ToString = function SystemEx_JSConvert$int64ToString(value, toBase) {
    /// <param name="value" type="Number" integer="true">
    /// </param>
    /// <param name="toBase" type="Number" integer="true">
    /// </param>
    /// <returns type="String"></returns>
    return value.toString();
}
SystemEx.JSConvert.bytesToJSArray = function SystemEx_JSConvert$bytesToJSArray(data) {
    /// <param name="data" type="Array" elementType="Number" elementInteger="true">
    /// </param>
    /// <returns type="JSArrayInteger"></returns>
    var jsan = [];
    var length = data.length;
    for (var index = length - 1; index >= 0; index--) {
        jsan.set(index, data[index]);
    }
    return jsan;
}
SystemEx.JSConvert.uBytesToJSArray = function SystemEx_JSConvert$uBytesToJSArray(data) {
    /// <param name="data" type="Array" elementType="Number" elementInteger="true">
    /// </param>
    /// <returns type="JSArrayInteger"></returns>
    var jsan = [];
    var length = data.length;
    for (var index = length - 1; index >= 0; index--) {
        jsan.set(index, data[index] & 255);
    }
    return jsan;
}
SystemEx.JSConvert.singlesToJSArray = function SystemEx_JSConvert$singlesToJSArray(data) {
    /// <param name="data" type="Array" elementType="Number">
    /// </param>
    /// <returns type="JSArrayNumber"></returns>
    var jsan = [];
    var length = data.length;
    for (var index = length - 1; index >= 0; index--) {
        jsan.set(index, data[index]);
    }
    return jsan;
}
SystemEx.JSConvert.doublesToJSArray = function SystemEx_JSConvert$doublesToJSArray(data) {
    /// <param name="data" type="Array" elementType="Number">
    /// </param>
    /// <returns type="JSArrayNumber"></returns>
    var jsan = [];
    var length = data.length;
    for (var index = length - 1; index >= 0; index--) {
        jsan.set(index, data[index]);
    }
    return jsan;
}
SystemEx.JSConvert.ints16ToJSArray = function SystemEx_JSConvert$ints16ToJSArray(data) {
    /// <param name="data" type="Array" elementType="Number" elementInteger="true">
    /// </param>
    /// <returns type="JSArrayInteger"></returns>
    var jsan = [];
    var length = data.length;
    for (var index = length - 1; index >= 0; index--) {
        jsan.set(index, data[index]);
    }
    return jsan;
}
SystemEx.JSConvert.uInt16ToJSArray = function SystemEx_JSConvert$uInt16ToJSArray(data) {
    /// <param name="data" type="Array" elementType="Number" elementInteger="true">
    /// </param>
    /// <returns type="JSArrayInteger"></returns>
    var jsan = [];
    var length = data.length;
    for (var index = length - 1; index >= 0; index--) {
        jsan.set(index, data[index] & 65535);
    }
    return jsan;
}
SystemEx.JSConvert.ints32ToJSArray = function SystemEx_JSConvert$ints32ToJSArray(data) {
    /// <param name="data" type="Array" elementType="Number" elementInteger="true">
    /// </param>
    /// <returns type="JSArrayInteger"></returns>
    var jsan = [];
    var length = data.length;
    for (var index = length - 1; index >= 0; index--) {
        jsan.set(index, data[index]);
    }
    return jsan;
}


////////////////////////////////////////////////////////////////////////////////
// SystemEx.StringBuilderEx

SystemEx.StringBuilderEx = function SystemEx_StringBuilderEx() {
}
SystemEx.StringBuilderEx.getLength = function SystemEx_StringBuilderEx$getLength(b) {
    /// <param name="b" type="ss.StringBuilder">
    /// </param>
    /// <returns type="Number" integer="true"></returns>
    return b.length;
}
SystemEx.StringBuilderEx.setLength = function SystemEx_StringBuilderEx$setLength(b, value) {
    /// <param name="b" type="ss.StringBuilder">
    /// </param>
    /// <param name="value" type="Number" integer="true">
    /// </param>
    b.length = value;
}


Type.registerNamespace('SystemEx.Html');

////////////////////////////////////////////////////////////////////////////////
// SystemEx.Html.CloseEventArgs

SystemEx.Html.CloseEventArgs = function SystemEx_Html_CloseEventArgs() {
}
SystemEx.Html.CloseEventArgs.prototype = {
    
    get_wasClean: function SystemEx_Html_CloseEventArgs$get_wasClean() {
        /// <value type="Boolean"></value>
        return this.wasClean;
    },
    
    initCloseEvent: function SystemEx_Html_CloseEventArgs$initCloseEvent(typeArg, canBubbleArg, cancelableArg, wasCleanArg) {
        /// <param name="typeArg" type="String">
        /// </param>
        /// <param name="canBubbleArg" type="Boolean">
        /// </param>
        /// <param name="cancelableArg" type="Boolean">
        /// </param>
        /// <param name="wasCleanArg" type="Boolean">
        /// </param>
        this.initCloseEvent(typeArg, canBubbleArg, cancelableArg, wasCleanArg);
    }
}


////////////////////////////////////////////////////////////////////////////////
// SystemEx.Html.MessageEventArgs

SystemEx.Html.MessageEventArgs = function SystemEx_Html_MessageEventArgs() {
}
SystemEx.Html.MessageEventArgs.prototype = {
    
    get_data: function SystemEx_Html_MessageEventArgs$get_data() {
        /// <value type="String"></value>
        return this.data;
    }
}


////////////////////////////////////////////////////////////////////////////////
// SystemEx.Html.WebSocket

SystemEx.Html.WebSocket = function SystemEx_Html_WebSocket() {
    /// <field name="CONNECTING" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="OPEN" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="CLOSING" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="CLOSED" type="Number" integer="true" static="true">
    /// </field>
}
SystemEx.Html.WebSocket.create = function SystemEx_Html_WebSocket$create(url, protocol) {
    /// <param name="url" type="String">
    /// </param>
    /// <param name="protocol" type="Object">
    /// </param>
    /// <returns type="SystemEx.Html.WebSocket"></returns>
    return ((protocol == null) ? new WebSocket(url) : new WebSocket(url, protocol));
}
SystemEx.Html.WebSocket.prototype = {
    
    get_url: function SystemEx_Html_WebSocket$get_url() {
        /// <value type="String"></value>
        return this.url;
    },
    
    get_readyState: function SystemEx_Html_WebSocket$get_readyState() {
        /// <value type="Number" integer="true"></value>
        return this.readyState;
    },
    
    get_bufferedAmount: function SystemEx_Html_WebSocket$get_bufferedAmount() {
        /// <value type="Number" integer="true"></value>
        return this.bufferedAmount;
    },
    
    get_onOpen: function SystemEx_Html_WebSocket$get_onOpen() {
        /// <value type="EventHandler"></value>
        return this.onopen;
    },
    set_onOpen: function SystemEx_Html_WebSocket$set_onOpen(value) {
        /// <value type="EventHandler"></value>
        this.onopen = value;
        return value;
    },
    
    get_onMessage: function SystemEx_Html_WebSocket$get_onMessage() {
        /// <value type="SystemEx.Html.MessageEventHandler"></value>
        return this.onmessage;
    },
    set_onMessage: function SystemEx_Html_WebSocket$set_onMessage(value) {
        /// <value type="SystemEx.Html.MessageEventHandler"></value>
        this.onmessage = value;
        return value;
    },
    
    get_onError: function SystemEx_Html_WebSocket$get_onError() {
        /// <value type="EventHandler"></value>
        return this.onerror;
    },
    set_onError: function SystemEx_Html_WebSocket$set_onError(value) {
        /// <value type="EventHandler"></value>
        this.onerror = value;
        return value;
    },
    
    get_onClose: function SystemEx_Html_WebSocket$get_onClose() {
        /// <value type="SystemEx.Html.CloseEventHandler"></value>
        return this.onclose;
    },
    set_onClose: function SystemEx_Html_WebSocket$set_onClose(value) {
        /// <value type="SystemEx.Html.CloseEventHandler"></value>
        this.onclose = value;
        return value;
    },
    
    get_protocol: function SystemEx_Html_WebSocket$get_protocol() {
        /// <value type="String"></value>
        return this.protocol;
    },
    
    send: function SystemEx_Html_WebSocket$send(data) {
        /// <param name="data" type="String">
        /// </param>
        /// <returns type="Boolean"></returns>
        return this.send(data);
    },
    
    close: function SystemEx_Html_WebSocket$close() {
        this.close();
    }
}


////////////////////////////////////////////////////////////////////////////////
// SystemEx.Html.LocalStorage

SystemEx.Html.LocalStorage = function SystemEx_Html_LocalStorage() {
}
SystemEx.Html.LocalStorage.getItem = function SystemEx_Html_LocalStorage$getItem(key) {
    /// <param name="key" type="String">
    /// </param>
    /// <returns type="String"></returns>
    try {
        return window.localStorage.getItem(key);
    }
    catch (e) {
        throw new Error('IOException:' + e);
    }
}
SystemEx.Html.LocalStorage.key = function SystemEx_Html_LocalStorage$key(index) {
    /// <param name="index" type="Number" integer="true">
    /// </param>
    /// <returns type="String"></returns>
    try {
        return window.localStorage.key(index);
    }
    catch (e) {
        throw new Error('IOException:' + e);
    }
}
SystemEx.Html.LocalStorage.get_length = function SystemEx_Html_LocalStorage$get_length() {
    /// <value type="Number" integer="true"></value>
    try {
        return window.localStorage.length;
    }
    catch (e) {
        throw new Error('IOException:' + e);
    }
}
SystemEx.Html.LocalStorage.removeItem = function SystemEx_Html_LocalStorage$removeItem(key) {
    /// <param name="key" type="String">
    /// </param>
    try {
        window.localStorage.removeItem(key);
    }
    catch (e) {
        throw new Error('IOException:' + e);
    }
}
SystemEx.Html.LocalStorage.setItem = function SystemEx_Html_LocalStorage$setItem(key, value) {
    /// <param name="key" type="String">
    /// </param>
    /// <param name="value" type="String">
    /// </param>
    try {
        window.localStorage.setItem(key, value);
    }
    catch (e) {
        throw new Error('IOException:' + e);
    }
}


Type.registerNamespace('SystemEx.Interop.CSyntax');

////////////////////////////////////////////////////////////////////////////////
// SystemEx.Interop.CSyntax._conversionSpecification

SystemEx.Interop.CSyntax._conversionSpecification = function SystemEx_Interop_CSyntax__conversionSpecification(fmtArg) {
    /// <param name="fmtArg" type="String">
    /// </param>
    /// <field name="_thousands" type="Boolean">
    /// </field>
    /// <field name="_leftJustify" type="Boolean">
    /// </field>
    /// <field name="_leadingSign" type="Boolean">
    /// </field>
    /// <field name="_leadingSpace" type="Boolean">
    /// </field>
    /// <field name="_alternateForm" type="Boolean">
    /// </field>
    /// <field name="_leadingZeros" type="Boolean">
    /// </field>
    /// <field name="_variableFieldWidth" type="Boolean">
    /// </field>
    /// <field name="_fieldWidth" type="Number" integer="true">
    /// </field>
    /// <field name="_fieldWidthSet" type="Boolean">
    /// </field>
    /// <field name="_precision" type="Number" integer="true">
    /// </field>
    /// <field name="_defaultDigits" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="_variablePrecision" type="Boolean">
    /// </field>
    /// <field name="_precisionSet" type="Boolean">
    /// </field>
    /// <field name="_positionalSpecification" type="Boolean">
    /// </field>
    /// <field name="_argumentPosition" type="Number" integer="true">
    /// </field>
    /// <field name="_positionalFieldWidth" type="Boolean">
    /// </field>
    /// <field name="_argumentPositionForFieldWidth" type="Number" integer="true">
    /// </field>
    /// <field name="_positionalPrecision" type="Boolean">
    /// </field>
    /// <field name="_argumentPositionForPrecision" type="Number" integer="true">
    /// </field>
    /// <field name="_optionalh" type="Boolean">
    /// </field>
    /// <field name="_optionall" type="Boolean">
    /// </field>
    /// <field name="_optionalL" type="Boolean">
    /// </field>
    /// <field name="_conversionCharacter" type="String">
    /// </field>
    /// <field name="_pos" type="Number" integer="true">
    /// </field>
    /// <field name="_fmt" type="String">
    /// </field>
    if (fmtArg == null) {
        throw new Error('ArgumentNullException: fmtArg');
    }
    if (fmtArg.charAt(0) === '%') {
        this._fmt = fmtArg;
        this._pos = 1;
        this._setArgPosition();
        this._setFlagCharacters();
        this._setFieldWidth();
        this._setPrecision();
        this._setOptionalHL();
        if (this._setConversionCharacter()) {
            if (this._pos === fmtArg.length) {
                if (this._leadingZeros && this._leftJustify) {
                    this._leadingZeros = false;
                }
                if (this._precisionSet && this._leadingZeros) {
                    if (this._conversionCharacter === 'd' || this._conversionCharacter === 'i' || this._conversionCharacter === 'o' || this._conversionCharacter === 'x') {
                        this._leadingZeros = false;
                    }
                }
            }
            else {
                throw new Error('ArgumentException: fmtArg: Malformed conversion specification=' + fmtArg);
            }
        }
        else {
            throw new Error('ArgumentException: fmtArg: Malformed conversion specification=' + fmtArg);
        }
    }
    else {
        throw new Error('ArgumentException: fmtArg: Control strings must begin with %.');
    }
}
SystemEx.Interop.CSyntax._conversionSpecification.prototype = {
    _thousands: false,
    _leftJustify: false,
    _leadingSign: false,
    _leadingSpace: false,
    _alternateForm: false,
    _leadingZeros: false,
    _variableFieldWidth: false,
    _fieldWidth: 0,
    _fieldWidthSet: false,
    _precision: 0,
    _variablePrecision: false,
    _precisionSet: false,
    _positionalSpecification: false,
    _argumentPosition: 0,
    _positionalFieldWidth: false,
    _argumentPositionForFieldWidth: 0,
    _positionalPrecision: false,
    _argumentPositionForPrecision: 0,
    _optionalh: false,
    _optionall: false,
    _optionalL: false,
    _conversionCharacter: '\u0000',
    _pos: 0,
    _fmt: null,
    
    _setLiteral: function SystemEx_Interop_CSyntax__conversionSpecification$_setLiteral(s) {
        /// <param name="s" type="String">
        /// </param>
        this._fmt = s;
    },
    
    _getLiteral: function SystemEx_Interop_CSyntax__conversionSpecification$_getLiteral() {
        /// <returns type="String"></returns>
        var sb = new ss.StringBuilder();
        var i = 0;
        while (i < this._fmt.length) {
            if (this._fmt.charAt(i) === '\\') {
                i++;
                if (i < this._fmt.length) {
                    var c = this._fmt.charAt(i);
                    switch (c) {
                        case 'a':
                            sb.append(7);
                            break;
                        case 'b':
                            sb.append('\u0008');
                            break;
                        case 'f':
                            sb.append('\u000c');
                            break;
                        case 'n':
                            sb.append('\n');
                            break;
                        case 'r':
                            sb.append('\r');
                            break;
                        case 't':
                            sb.append('\t');
                            break;
                        case 'v':
                            sb.append(11);
                            break;
                        case '\\':
                            sb.append('\\');
                            break;
                    }
                    i++;
                }
                else {
                    sb.append('\\');
                }
            }
            else {
                i++;
            }
        }
        return this._fmt;
    },
    
    _getConversionCharacter: function SystemEx_Interop_CSyntax__conversionSpecification$_getConversionCharacter() {
        /// <returns type="String"></returns>
        return this._conversionCharacter;
    },
    
    _isVariableFieldWidth: function SystemEx_Interop_CSyntax__conversionSpecification$_isVariableFieldWidth() {
        /// <returns type="Boolean"></returns>
        return this._variableFieldWidth;
    },
    
    _setFieldWidthWithArg: function SystemEx_Interop_CSyntax__conversionSpecification$_setFieldWidthWithArg(fw) {
        /// <param name="fw" type="Number" integer="true">
        /// </param>
        if (fw < 0) {
            this._leftJustify = true;
        }
        this._fieldWidthSet = true;
        this._fieldWidth = Math.abs(fw);
    },
    
    _isVariablePrecision: function SystemEx_Interop_CSyntax__conversionSpecification$_isVariablePrecision() {
        /// <returns type="Boolean"></returns>
        return this._variablePrecision;
    },
    
    _setPrecisionWithArg: function SystemEx_Interop_CSyntax__conversionSpecification$_setPrecisionWithArg(pr) {
        /// <param name="pr" type="Number" integer="true">
        /// </param>
        this._precisionSet = true;
        this._precision = Math.max(pr, 0);
    },
    
    _internalsprintfInt32: function SystemEx_Interop_CSyntax__conversionSpecification$_internalsprintfInt32(s) {
        /// <param name="s" type="Number" integer="true">
        /// </param>
        /// <returns type="String"></returns>
        var s2 = '';
        switch (this._conversionCharacter) {
            case 'd':
            case 'i':
                if (this._optionalh) {
                    s2 = this._printDFormatInt16(s);
                }
                else if (this._optionall) {
                    s2 = this._printDFormatInt64(s);
                }
                else {
                    s2 = this._printDFormatInt32(s);
                }
                break;
            case 'x':
            case 'X':
                if (this._optionalh) {
                    s2 = this._printXFormatInt16(s);
                }
                else if (this._optionall) {
                    s2 = this._printXFormatInt64(s);
                }
                else {
                    s2 = this._printXFormatInt32(s);
                }
                break;
            case 'o':
                if (this._optionalh) {
                    s2 = this._printOFormatInt16(s);
                }
                else if (this._optionall) {
                    s2 = this._printOFormatInt64(s);
                }
                else {
                    s2 = this._printOFormatInt32(s);
                }
                break;
            case 'c':
            case 'C':
                s2 = this._printCFormat(s);
                break;
            default:
                throw new Error('ArgumentException: conversionCharacter: Cannot format a int with a format using a ' + this._conversionCharacter + ' conversion character.');
        }
        return s2;
    },
    
    _internalsprintfInt64: function SystemEx_Interop_CSyntax__conversionSpecification$_internalsprintfInt64(s) {
        /// <param name="s" type="Number" integer="true">
        /// </param>
        /// <returns type="String"></returns>
        var s2 = '';
        switch (this._conversionCharacter) {
            case 'd':
            case 'i':
                if (this._optionalh) {
                    s2 = this._printDFormatInt16(s);
                }
                else if (this._optionall) {
                    s2 = this._printDFormatInt64(s);
                }
                else {
                    s2 = this._printDFormatInt32(s);
                }
                break;
            case 'x':
            case 'X':
                if (this._optionalh) {
                    s2 = this._printXFormatInt16(s);
                }
                else if (this._optionall) {
                    s2 = this._printXFormatInt64(s);
                }
                else {
                    s2 = this._printXFormatInt32(s);
                }
                break;
            case 'o':
                if (this._optionalh) {
                    s2 = this._printOFormatInt16(s);
                }
                else if (this._optionall) {
                    s2 = this._printOFormatInt64(s);
                }
                else {
                    s2 = this._printOFormatInt32(s);
                }
                break;
            case 'c':
            case 'C':
                s2 = this._printCFormat(s);
                break;
            default:
                throw new Error('ArgumentException: conversionCharacter: Cannot format a long with a format using a ' + this._conversionCharacter + ' conversion character.');
        }
        return s2;
    },
    
    _internalsprintfDouble: function SystemEx_Interop_CSyntax__conversionSpecification$_internalsprintfDouble(s) {
        /// <param name="s" type="Number">
        /// </param>
        /// <returns type="String"></returns>
        var s2 = '';
        switch (this._conversionCharacter) {
            case 'f':
                s2 = this._printFFormat(s);
                break;
            case 'E':
            case 'e':
                s2 = this._printEFormat(s);
                break;
            case 'G':
            case 'g':
                s2 = this._printGFormat(s);
                break;
            default:
                throw new Error('ArgumentException: ConversionCharacter: Cannot ' + 'format a double with a format using a ' + this._conversionCharacter + ' conversion character.');
        }
        return s2;
    },
    
    _internalsprintfString: function SystemEx_Interop_CSyntax__conversionSpecification$_internalsprintfString(s) {
        /// <param name="s" type="String">
        /// </param>
        /// <returns type="String"></returns>
        var s2 = '';
        if (this._conversionCharacter === 's' || this._conversionCharacter === 'S') {
            s2 = this._printSFormat(s);
        }
        else {
            throw new Error('ArgumentException: ConversionCharacter: Cannot ' + 'format a String with a format using a ' + this._conversionCharacter + ' conversion character.');
        }
        return s2;
    },
    
    _internalsprintfObject: function SystemEx_Interop_CSyntax__conversionSpecification$_internalsprintfObject(s) {
        /// <param name="s" type="Object">
        /// </param>
        /// <returns type="String"></returns>
        var s2 = '';
        if (this._conversionCharacter === 's' || this._conversionCharacter === 'S') {
            s2 = this._printSFormat(s.toString());
        }
        else {
            throw new Error('ArgumentException: ConversionCharacter: Cannot format a String with a format using' + ' a ' + this._conversionCharacter + ' conversion character.');
        }
        return s2;
    },
    
    _fFormatDigits: function SystemEx_Interop_CSyntax__conversionSpecification$_fFormatDigits(x) {
        /// <param name="x" type="Number">
        /// </param>
        /// <returns type="Array" elementType="String"></returns>
        var sx;
        var i, j, k;
        var n1In, n2In;
        var expon = 0;
        var minusSign = false;
        if (x > 0) {
            sx = x.toString();
        }
        else if (x < 0) {
            sx = (-x).toString();
            minusSign = true;
        }
        else {
            sx = x.toString();
            if (sx.charAt(0) === '-') {
                minusSign = true;
                sx = sx.substring(1, sx.length);
            }
        }
        var ePos = sx.indexOf('E');
        var rPos = sx.indexOf('.');
        if (rPos !== -1) {
            n1In = rPos;
        }
        else if (ePos !== -1) {
            n1In = ePos;
        }
        else {
            n1In = sx.length;
        }
        if (rPos !== -1) {
            if (ePos !== -1) {
                n2In = ePos - rPos - 1;
            }
            else {
                n2In = sx.length - rPos - 1;
            }
        }
        else {
            n2In = 0;
        }
        if (ePos !== -1) {
            var ie = ePos + 1;
            expon = 0;
            if (sx.charAt(ie) === '-') {
                for (++ie; ie < sx.length; ie++) {
                    if (sx.charAt(ie) !== '0') {
                        break;
                    }
                }
                if (ie < sx.length) {
                    expon = -parseInt(sx.substring(ie, sx.length));
                }
            }
            else {
                if (sx.charAt(ie) === '+') {
                    ++ie;
                }
                for (; ie < sx.length; ie++) {
                    if (sx.charAt(ie) !== '0') {
                        break;
                    }
                }
                if (ie < sx.length) {
                    expon = parseInt(sx.substring(ie, sx.length));
                }
            }
        }
        var p;
        if (this._precisionSet) {
            p = this._precision;
        }
        else {
            p = SystemEx.Interop.CSyntax._conversionSpecification._defaultDigits - 1;
        }
        var ca1 = SystemEx.JSString.stringToChars(sx);
        var ca2 = new Array(n1In + n2In);
        var ca3, ca4, ca5;
        for (j = 0; j < n1In; j++) {
            ca2[j] = ca1[j];
        }
        i = j + 1;
        for (k = 0; k < n2In; j++, i++, k++) {
            ca2[j] = ca1[i];
        }
        if (n1In + expon <= 0) {
            ca3 = new Array(-expon + n2In);
            for (j = 0, k = 0; k < (-n1In - expon); k++, j++) {
                ca3[j] = '0';
            }
            for (i = 0; i < (n1In + n2In); i++, j++) {
                ca3[j] = ca2[i];
            }
        }
        else {
            ca3 = ca2;
        }
        var carry = false;
        if (p < -expon + n2In) {
            if (expon < 0) {
                i = p;
            }
            else {
                i = p + n1In;
            }
            carry = this._checkForCarry(ca3, i);
            if (carry) {
                carry = this._startSymbolicCarry(ca3, i - 1, 0);
            }
        }
        if (n1In + expon <= 0) {
            ca4 = new Array(2 + p);
            if (!carry) {
                ca4[0] = '0';
            }
            else {
                ca4[0] = '1';
            }
            if (this._alternateForm || !this._precisionSet || this._precision !== 0) {
                ca4[1] = '.';
                for (i = 0, j = 2; i < Math.min(p, ca3.length); i++, j++) {
                    ca4[j] = ca3[i];
                }
                for (; j < ca4.length; j++) {
                    ca4[j] = '0';
                }
            }
        }
        else {
            if (!carry) {
                if (this._alternateForm || !this._precisionSet || this._precision !== 0) {
                    ca4 = new Array(n1In + expon + p + 1);
                }
                else {
                    ca4 = new Array(n1In + expon);
                }
                j = 0;
            }
            else {
                if (this._alternateForm || !this._precisionSet || this._precision !== 0) {
                    ca4 = new Array(n1In + expon + p + 2);
                }
                else {
                    ca4 = new Array(n1In + expon + 1);
                }
                ca4[0] = '1';
                j = 1;
            }
            for (i = 0; i < Math.min(n1In + expon, ca3.length); i++, j++) {
                ca4[j] = ca3[i];
            }
            for (; i < n1In + expon; i++, j++) {
                ca4[j] = '0';
            }
            if (this._alternateForm || !this._precisionSet || this._precision !== 0) {
                ca4[j] = '.';
                j++;
                for (k = 0; i < ca3.length && k < p; i++, j++, k++) {
                    ca4[j] = ca3[i];
                }
                for (; j < ca4.length; j++) {
                    ca4[j] = '0';
                }
            }
        }
        var nZeros = 0;
        if (!this._leftJustify && this._leadingZeros) {
            var xThousands = 0;
            if (this._thousands) {
                var xlead = 0;
                if (ca4[0] === '+' || ca4[0] === '-' || ca4[0] === ' ') {
                    xlead = 1;
                }
                var xdp = xlead;
                for (; xdp < ca4.length; xdp++) {
                    if (ca4[xdp] === '.') {
                        break;
                    }
                }
                xThousands = (xdp - xlead) / 3;
            }
            if (this._fieldWidthSet) {
                nZeros = this._fieldWidth - ca4.length;
            }
            if ((!minusSign && (this._leadingSign || this._leadingSpace)) || minusSign) {
                nZeros--;
            }
            nZeros -= xThousands;
            if (nZeros < 0) {
                nZeros = 0;
            }
        }
        j = 0;
        if ((!minusSign && (this._leadingSign || this._leadingSpace)) || minusSign) {
            ca5 = new Array(ca4.length + nZeros + 1);
            j++;
        }
        else {
            ca5 = new Array(ca4.length + nZeros);
        }
        if (!minusSign) {
            if (this._leadingSign) {
                ca5[0] = '+';
            }
            if (this._leadingSpace) {
                ca5[0] = ' ';
            }
        }
        else {
            ca5[0] = '-';
        }
        for (i = 0; i < nZeros; i++, j++) {
            ca5[j] = '0';
        }
        for (i = 0; i < ca4.length; i++, j++) {
            ca5[j] = ca4[i];
        }
        var lead = 0;
        if (ca5[0] === '+' || ca5[0] === '-' || ca5[0] === ' ') {
            lead = 1;
        }
        var dp = lead;
        for (; dp < ca5.length; dp++) {
            if (ca5[dp] === '.') {
                break;
            }
        }
        var nThousands = (dp - lead) / 3;
        if (dp < ca5.length) {
            ca5[dp] = '.';
        }
        var ca6 = ca5;
        if (this._thousands && nThousands > 0) {
            ca6 = new Array(ca5.length + nThousands + lead);
            ca6[0] = ca5[0];
            for (i = lead, k = lead; i < dp; i++) {
                if (i > 0 && (dp - i) % 3 === 0) {
                    ca6[k] = ',';
                    ca6[k + 1] = ca5[i];
                    k += 2;
                }
                else {
                    ca6[k] = ca5[i];
                    k++;
                }
            }
            for (; i < ca5.length; i++, k++) {
                ca6[k] = ca5[i];
            }
        }
        return ca6;
    },
    
    _fFormatString: function SystemEx_Interop_CSyntax__conversionSpecification$_fFormatString(x) {
        /// <param name="x" type="Number">
        /// </param>
        /// <returns type="String"></returns>
        var ca6, ca7;
        if (!isFinite(x)) {
            if (x === Number.POSITIVE_INFINITY) {
                if (this._leadingSign) {
                    ca6 = SystemEx.JSString.stringToChars('+Inf');
                }
                else if (this._leadingSpace) {
                    ca6 = SystemEx.JSString.stringToChars(' Inf');
                }
                else {
                    ca6 = SystemEx.JSString.stringToChars('Inf');
                }
            }
            else {
                ca6 = SystemEx.JSString.stringToChars('-Inf');
            }
        }
        else if (isNaN(x)) {
            if (this._leadingSign) {
                ca6 = SystemEx.JSString.stringToChars('+NaN');
            }
            else if (this._leadingSpace) {
                ca6 = SystemEx.JSString.stringToChars(' NaN');
            }
            else {
                ca6 = SystemEx.JSString.stringToChars('NaN');
            }
        }
        else {
            ca6 = this._fFormatDigits(x);
        }
        ca7 = this._applyFloatPadding(ca6, false);
        return SystemEx.JSString.charsToString(ca7);
    },
    
    _eFormatDigits: function SystemEx_Interop_CSyntax__conversionSpecification$_eFormatDigits(x, eChar) {
        /// <param name="x" type="Number">
        /// </param>
        /// <param name="eChar" type="String">
        /// </param>
        /// <returns type="Array" elementType="String"></returns>
        var ca1, ca2, ca3;
        var sx;
        var i, j, k, p;
        var n1In, n2In;
        var expon = 0;
        var ePos, rPos, eSize;
        var minusSign = false;
        if (x > 0) {
            sx = x.toString();
        }
        else if (x < 0) {
            sx = (-x).toString();
            minusSign = true;
        }
        else {
            sx = x.toString();
            if (sx.charAt(0) === '-') {
                minusSign = true;
                sx = sx.substring(1, sx.length);
            }
        }
        ePos = sx.indexOf('E');
        if (ePos === -1) {
            ePos = sx.indexOf('e');
        }
        rPos = sx.indexOf('.');
        if (rPos !== -1) {
            n1In = rPos;
        }
        else if (ePos !== -1) {
            n1In = ePos;
        }
        else {
            n1In = sx.length;
        }
        if (rPos !== -1) {
            if (ePos !== -1) {
                n2In = ePos - rPos - 1;
            }
            else {
                n2In = sx.length - rPos - 1;
            }
        }
        else {
            n2In = 0;
        }
        if (ePos !== -1) {
            var ie = ePos + 1;
            expon = 0;
            if (sx.charAt(ie) === '-') {
                for (++ie; ie < sx.length; ie++) {
                    if (sx.charAt(ie) !== '0') {
                        break;
                    }
                }
                if (ie < sx.length) {
                    expon = -parseInt(sx.substring(ie, sx.length));
                }
            }
            else {
                if (sx.charAt(ie) === '+') {
                    ++ie;
                }
                for (; ie < sx.length; ie++) {
                    if (sx.charAt(ie) !== '0') {
                        break;
                    }
                }
                if (ie < sx.length) {
                    expon = parseInt(sx.substring(ie, sx.length));
                }
            }
        }
        if (rPos !== -1) {
            expon += rPos - 1;
        }
        if (this._precisionSet) {
            p = this._precision;
        }
        else {
            p = SystemEx.Interop.CSyntax._conversionSpecification._defaultDigits - 1;
        }
        if (rPos !== -1 && ePos !== -1) {
            ca1 = SystemEx.JSString.stringToChars(sx.substring(0, rPos - 0) + sx.substring(rPos + 1, ePos - rPos + 1));
        }
        else if (rPos !== -1) {
            ca1 = SystemEx.JSString.stringToChars(sx.substring(0, rPos - 0) + sx.substring(rPos + 1, sx.length));
        }
        else if (ePos !== -1) {
            ca1 = SystemEx.JSString.stringToChars(sx.substring(0, ePos - 0));
        }
        else {
            ca1 = SystemEx.JSString.stringToChars(sx);
        }
        var carry = false;
        var i0 = 0;
        if (ca1[0] !== '0') {
            i0 = 0;
        }
        else {
            for (i0 = 0; i0 < ca1.length; i0++) {
                if (ca1[i0] !== '0') {
                    break;
                }
            }
        }
        if (i0 + p < ca1.length - 1) {
            carry = this._checkForCarry(ca1, i0 + p + 1);
            if (carry) {
                carry = this._startSymbolicCarry(ca1, i0 + p, i0);
            }
            if (carry) {
                ca2 = new Array(i0 + p + 1);
                ca2[i0] = '1';
                for (j = 0; j < i0; j++) {
                    ca2[j] = '0';
                }
                for (i = i0, j = i0 + 1; j < p + 1; i++, j++) {
                    ca2[j] = ca1[i];
                }
                expon++;
                ca1 = ca2;
            }
        }
        if (Math.abs(expon) < 100 && !this._optionalL) {
            eSize = 4;
        }
        else {
            eSize = 5;
        }
        if (this._alternateForm || !this._precisionSet || this._precision !== 0) {
            ca2 = new Array(2 + p + eSize);
        }
        else {
            ca2 = new Array(1 + eSize);
        }
        if (ca1[0] !== '0') {
            ca2[0] = ca1[0];
            j = 1;
        }
        else {
            for (j = 1; j < ((ePos === -1) ? ca1.length : ePos); j++) {
                if (ca1[j] !== '0') {
                    break;
                }
            }
            if ((ePos !== -1 && j < ePos) || (ePos === -1 && j < ca1.length)) {
                ca2[0] = ca1[j];
                expon -= j;
                j++;
            }
            else {
                ca2[0] = '0';
                j = 2;
            }
        }
        if (this._alternateForm || !this._precisionSet || this._precision !== 0) {
            ca2[1] = '.';
            i = 2;
        }
        else {
            i = 1;
        }
        for (k = 0; k < p && j < ca1.length; j++, i++, k++) {
            ca2[i] = ca1[j];
        }
        for (; i < ca2.length - eSize; i++) {
            ca2[i] = '0';
        }
        ca2[i++] = eChar;
        if (expon < 0) {
            ca2[i++] = '-';
        }
        else {
            ca2[i++] = '+';
        }
        expon = Math.abs(expon);
        if (expon >= 100) {
            switch (expon / 100) {
                case 1:
                    ca2[i] = '1';
                    break;
                case 2:
                    ca2[i] = '2';
                    break;
                case 3:
                    ca2[i] = '3';
                    break;
                case 4:
                    ca2[i] = '4';
                    break;
                case 5:
                    ca2[i] = '5';
                    break;
                case 6:
                    ca2[i] = '6';
                    break;
                case 7:
                    ca2[i] = '7';
                    break;
                case 8:
                    ca2[i] = '8';
                    break;
                case 9:
                    ca2[i] = '9';
                    break;
            }
            i++;
        }
        switch ((expon % 100) / 10) {
            case 0:
                ca2[i] = '0';
                break;
            case 1:
                ca2[i] = '1';
                break;
            case 2:
                ca2[i] = '2';
                break;
            case 3:
                ca2[i] = '3';
                break;
            case 4:
                ca2[i] = '4';
                break;
            case 5:
                ca2[i] = '5';
                break;
            case 6:
                ca2[i] = '6';
                break;
            case 7:
                ca2[i] = '7';
                break;
            case 8:
                ca2[i] = '8';
                break;
            case 9:
                ca2[i] = '9';
                break;
        }
        i++;
        switch (expon % 10) {
            case 0:
                ca2[i] = '0';
                break;
            case 1:
                ca2[i] = '1';
                break;
            case 2:
                ca2[i] = '2';
                break;
            case 3:
                ca2[i] = '3';
                break;
            case 4:
                ca2[i] = '4';
                break;
            case 5:
                ca2[i] = '5';
                break;
            case 6:
                ca2[i] = '6';
                break;
            case 7:
                ca2[i] = '7';
                break;
            case 8:
                ca2[i] = '8';
                break;
            case 9:
                ca2[i] = '9';
                break;
        }
        var nZeros = 0;
        if (!this._leftJustify && this._leadingZeros) {
            var xThousands = 0;
            if (this._thousands) {
                var xlead = 0;
                if (ca2[0] === '+' || ca2[0] === '-' || ca2[0] === ' ') {
                    xlead = 1;
                }
                var xdp = xlead;
                for (; xdp < ca2.length; xdp++) {
                    if (ca2[xdp] === '.') {
                        break;
                    }
                }
                xThousands = (xdp - xlead) / 3;
            }
            if (this._fieldWidthSet) {
                nZeros = this._fieldWidth - ca2.length;
            }
            if ((!minusSign && (this._leadingSign || this._leadingSpace)) || minusSign) {
                nZeros--;
            }
            nZeros -= xThousands;
            if (nZeros < 0) {
                nZeros = 0;
            }
        }
        j = 0;
        if ((!minusSign && (this._leadingSign || this._leadingSpace)) || minusSign) {
            ca3 = new Array(ca2.length + nZeros + 1);
            j++;
        }
        else {
            ca3 = new Array(ca2.length + nZeros);
        }
        if (!minusSign) {
            if (this._leadingSign) {
                ca3[0] = '+';
            }
            if (this._leadingSpace) {
                ca3[0] = ' ';
            }
        }
        else {
            ca3[0] = '-';
        }
        for (k = 0; k < nZeros; j++, k++) {
            ca3[j] = '0';
        }
        for (i = 0; i < ca2.length && j < ca3.length; i++, j++) {
            ca3[j] = ca2[i];
        }
        var lead = 0;
        if (ca3[0] === '+' || ca3[0] === '-' || ca3[0] === ' ') {
            lead = 1;
        }
        var dp = lead;
        for (; dp < ca3.length; dp++) {
            if (ca3[dp] === '.') {
                break;
            }
        }
        var nThousands = dp / 3;
        if (dp < ca3.length) {
            ca3[dp] = '.';
        }
        var ca4 = ca3;
        if (this._thousands && nThousands > 0) {
            ca4 = new Array(ca3.length + nThousands + lead);
            ca4[0] = ca3[0];
            for (i = lead, k = lead; i < dp; i++) {
                if (i > 0 && (dp - i) % 3 === 0) {
                    ca4[k] = ',';
                    ca4[k + 1] = ca3[i];
                    k += 2;
                }
                else {
                    ca4[k] = ca3[i];
                    k++;
                }
            }
            for (; i < ca3.length; i++, k++) {
                ca4[k] = ca3[i];
            }
        }
        return ca4;
    },
    
    _checkForCarry: function SystemEx_Interop_CSyntax__conversionSpecification$_checkForCarry(ca1, icarry) {
        /// <param name="ca1" type="Array" elementType="String">
        /// </param>
        /// <param name="icarry" type="Number" integer="true">
        /// </param>
        /// <returns type="Boolean"></returns>
        var carry = false;
        if (icarry < ca1.length) {
            if (ca1[icarry] === '6' || ca1[icarry] === '7' || ca1[icarry] === '8' || ca1[icarry] === '9') {
                carry = true;
            }
            else if (ca1[icarry] === '5') {
                var ii = icarry + 1;
                for (; ii < ca1.length; ii++) {
                    if (ca1[ii] !== '0') {
                        break;
                    }
                }
                carry = ii < ca1.length;
                if (!carry && icarry > 0) {
                    carry = (ca1[icarry - 1] === '1' || ca1[icarry - 1] === '3' || ca1[icarry - 1] === '5' || ca1[icarry - 1] === '7' || ca1[icarry - 1] === '9');
                }
            }
        }
        return carry;
    },
    
    _startSymbolicCarry: function SystemEx_Interop_CSyntax__conversionSpecification$_startSymbolicCarry(ca, cLast, cFirst) {
        /// <param name="ca" type="Array" elementType="String">
        /// </param>
        /// <param name="cLast" type="Number" integer="true">
        /// </param>
        /// <param name="cFirst" type="Number" integer="true">
        /// </param>
        /// <returns type="Boolean"></returns>
        var carry = true;
        for (var i = cLast; carry && i >= cFirst; i--) {
            carry = false;
            switch (ca[i]) {
                case '0':
                    ca[i] = '1';
                    break;
                case '1':
                    ca[i] = '2';
                    break;
                case '2':
                    ca[i] = '3';
                    break;
                case '3':
                    ca[i] = '4';
                    break;
                case '4':
                    ca[i] = '5';
                    break;
                case '5':
                    ca[i] = '6';
                    break;
                case '6':
                    ca[i] = '7';
                    break;
                case '7':
                    ca[i] = '8';
                    break;
                case '8':
                    ca[i] = '9';
                    break;
                case '9':
                    ca[i] = '0';
                    carry = true;
                    break;
            }
        }
        return carry;
    },
    
    _eFormatString: function SystemEx_Interop_CSyntax__conversionSpecification$_eFormatString(x, eChar) {
        /// <param name="x" type="Number">
        /// </param>
        /// <param name="eChar" type="String">
        /// </param>
        /// <returns type="String"></returns>
        var ca4, ca5;
        if (!isFinite(x)) {
            if (x === Number.POSITIVE_INFINITY) {
                if (this._leadingSign) {
                    ca4 = SystemEx.JSString.stringToChars('+Inf');
                }
                else if (this._leadingSpace) {
                    ca4 = SystemEx.JSString.stringToChars(' Inf');
                }
                else {
                    ca4 = SystemEx.JSString.stringToChars('Inf');
                }
            }
            else {
                ca4 = SystemEx.JSString.stringToChars('-Inf');
            }
        }
        else if (isNaN(x)) {
            if (this._leadingSign) {
                ca4 = SystemEx.JSString.stringToChars('+NaN');
            }
            else if (this._leadingSpace) {
                ca4 = SystemEx.JSString.stringToChars(' NaN');
            }
            else {
                ca4 = SystemEx.JSString.stringToChars('NaN');
            }
        }
        else {
            ca4 = this._eFormatDigits(x, eChar);
        }
        ca5 = this._applyFloatPadding(ca4, false);
        return SystemEx.JSString.charsToString(ca5);
    },
    
    _applyFloatPadding: function SystemEx_Interop_CSyntax__conversionSpecification$_applyFloatPadding(ca4, noDigits) {
        /// <param name="ca4" type="Array" elementType="String">
        /// </param>
        /// <param name="noDigits" type="Boolean">
        /// </param>
        /// <returns type="Array" elementType="String"></returns>
        var ca5 = ca4;
        if (this._fieldWidthSet) {
            var i, j, nBlanks;
            if (this._leftJustify) {
                nBlanks = this._fieldWidth - ca4.length;
                if (nBlanks > 0) {
                    ca5 = new Array(ca4.length + nBlanks);
                    for (i = 0; i < ca4.length; i++) {
                        ca5[i] = ca4[i];
                    }
                    for (j = 0; j < nBlanks; j++, i++) {
                        ca5[i] = ' ';
                    }
                }
            }
            else if (!this._leadingZeros || noDigits) {
                nBlanks = this._fieldWidth - ca4.length;
                if (nBlanks > 0) {
                    ca5 = new Array(ca4.length + nBlanks);
                    for (i = 0; i < nBlanks; i++) {
                        ca5[i] = ' ';
                    }
                    for (j = 0; j < ca4.length; i++, j++) {
                        ca5[i] = ca4[j];
                    }
                }
            }
            else if (this._leadingZeros) {
                nBlanks = this._fieldWidth - ca4.length;
                if (nBlanks > 0) {
                    ca5 = new Array(ca4.length + nBlanks);
                    i = 0;
                    j = 0;
                    if (ca4[0] === '-') {
                        ca5[0] = '-';
                        i++;
                        j++;
                    }
                    for (var k = 0; k < nBlanks; i++, k++) {
                        ca5[i] = '0';
                    }
                    for (; j < ca4.length; i++, j++) {
                        ca5[i] = ca4[j];
                    }
                }
            }
        }
        return ca5;
    },
    
    _printFFormat: function SystemEx_Interop_CSyntax__conversionSpecification$_printFFormat(x) {
        /// <param name="x" type="Number">
        /// </param>
        /// <returns type="String"></returns>
        return this._fFormatString(x);
    },
    
    _printEFormat: function SystemEx_Interop_CSyntax__conversionSpecification$_printEFormat(x) {
        /// <param name="x" type="Number">
        /// </param>
        /// <returns type="String"></returns>
        if (this._conversionCharacter === 'e') {
            return this._eFormatString(x, 'e');
        }
        else {
            return this._eFormatString(x, 'E');
        }
    },
    
    _printGFormat: function SystemEx_Interop_CSyntax__conversionSpecification$_printGFormat(x) {
        /// <param name="x" type="Number">
        /// </param>
        /// <returns type="String"></returns>
        var sx, sy, sz, ret;
        var savePrecision = this._precision;
        var i;
        var ca4, ca5;
        if (!isFinite(x)) {
            if (x === Number.POSITIVE_INFINITY) {
                if (this._leadingSign) {
                    ca4 = SystemEx.JSString.stringToChars('+Inf');
                }
                else if (this._leadingSpace) {
                    ca4 = SystemEx.JSString.stringToChars(' Inf');
                }
                else {
                    ca4 = SystemEx.JSString.stringToChars('Inf');
                }
            }
            else {
                ca4 = SystemEx.JSString.stringToChars('-Inf');
            }
        }
        else if (isNaN(x)) {
            if (this._leadingSign) {
                ca4 = SystemEx.JSString.stringToChars('+NaN');
            }
            else if (this._leadingSpace) {
                ca4 = SystemEx.JSString.stringToChars(' NaN');
            }
            else {
                ca4 = SystemEx.JSString.stringToChars('NaN');
            }
        }
        else {
            if (!this._precisionSet) {
                this._precision = SystemEx.Interop.CSyntax._conversionSpecification._defaultDigits;
            }
            if (this._precision === 0) {
                this._precision = 1;
            }
            var ePos = -1;
            if (this._conversionCharacter === 'g') {
                sx = this._eFormatString(x, 'e').trim();
                ePos = sx.indexOf('e');
            }
            else {
                sx = this._eFormatString(x, 'E').trim();
                ePos = sx.indexOf('E');
            }
            i = ePos + 1;
            var expon = 0;
            if (sx.charAt(i) === '-') {
                for (++i; i < sx.length; i++) {
                    if (sx.charAt(i) !== '0') {
                        break;
                    }
                }
                if (i < sx.length) {
                    expon = -parseInt(sx.substring(i, sx.length));
                }
            }
            else {
                if (sx.charAt(i) === '+') {
                    ++i;
                }
                for (; i < sx.length; i++) {
                    if (sx.charAt(i) !== '0') {
                        break;
                    }
                }
                if (i < sx.length) {
                    expon = parseInt(sx.substring(i, sx.length));
                }
            }
            if (!this._alternateForm) {
                if (expon >= -4 && expon < this._precision) {
                    sy = this._fFormatString(x).trim();
                }
                else {
                    sy = sx.substring(0, ePos - 0);
                }
                i = sy.length - 1;
                for (; i >= 0; i--) {
                    if (sy.charAt(i) !== '0') {
                        break;
                    }
                }
                if (i >= 0 && sy.charAt(i) === '.') {
                    i--;
                }
                if (i === -1) {
                    sz = '0';
                }
                else if (!SystemEx.JSConvert.char_IsDigit(sy.charAt(i))) {
                    sz = sy.substring(0, i + 1 - 0) + '0';
                }
                else {
                    sz = sy.substring(0, i + 1 - 0);
                }
                if (expon >= -4 && expon < this._precision) {
                    ret = sz;
                }
                else {
                    ret = sz + sx.substring(ePos, sx.length);
                }
            }
            else {
                if (expon >= -4 && expon < this._precision) {
                    ret = this._fFormatString(x).trim();
                }
                else {
                    ret = sx;
                }
            }
            if (this._leadingSpace) {
                if (x >= 0) {
                    ret = ' ' + ret;
                }
            }
            ca4 = SystemEx.JSString.stringToChars(ret);
        }
        ca5 = this._applyFloatPadding(ca4, false);
        this._precision = savePrecision;
        return SystemEx.JSString.charsToString(ca5);
    },
    
    _printDFormatInt16: function SystemEx_Interop_CSyntax__conversionSpecification$_printDFormatInt16(x) {
        /// <param name="x" type="Number" integer="true">
        /// </param>
        /// <returns type="String"></returns>
        return this._printDFormat(x.toString());
    },
    
    _printDFormatInt64: function SystemEx_Interop_CSyntax__conversionSpecification$_printDFormatInt64(x) {
        /// <param name="x" type="Number" integer="true">
        /// </param>
        /// <returns type="String"></returns>
        return this._printDFormat(x.toString());
    },
    
    _printDFormatInt32: function SystemEx_Interop_CSyntax__conversionSpecification$_printDFormatInt32(x) {
        /// <param name="x" type="Number" integer="true">
        /// </param>
        /// <returns type="String"></returns>
        return this._printDFormat(x.toString());
    },
    
    _printDFormat: function SystemEx_Interop_CSyntax__conversionSpecification$_printDFormat(sx) {
        /// <param name="sx" type="String">
        /// </param>
        /// <returns type="String"></returns>
        var nLeadingZeros = 0;
        var nBlanks = 0, n = 0;
        var i = 0, jFirst = 0;
        var neg = sx.charAt(0) === '-';
        if (sx === '0' && this._precisionSet && this._precision === 0) {
            sx = '';
        }
        if (!neg) {
            if (this._precisionSet && sx.length < this._precision) {
                nLeadingZeros = this._precision - sx.length;
            }
        }
        else {
            if (this._precisionSet && (sx.length - 1) < this._precision) {
                nLeadingZeros = this._precision - sx.length + 1;
            }
        }
        if (nLeadingZeros < 0) {
            nLeadingZeros = 0;
        }
        if (this._fieldWidthSet) {
            nBlanks = this._fieldWidth - nLeadingZeros - sx.length;
            if (!neg && (this._leadingSign || this._leadingSpace)) {
                nBlanks--;
            }
        }
        if (nBlanks < 0) {
            nBlanks = 0;
        }
        if (this._leadingSign) {
            n++;
        }
        else if (this._leadingSpace) {
            n++;
        }
        n += nBlanks;
        n += nLeadingZeros;
        n += sx.length;
        var ca = new Array(n);
        if (this._leftJustify) {
            if (neg) {
                ca[i++] = '-';
            }
            else if (this._leadingSign) {
                ca[i++] = '+';
            }
            else if (this._leadingSpace) {
                ca[i++] = ' ';
            }
            var csx = SystemEx.JSString.stringToChars(sx);
            jFirst = (neg) ? 1 : 0;
            for (var j = 0; j < nLeadingZeros; i++, j++) {
                ca[i] = '0';
            }
            for (var j = jFirst; j < csx.length; j++, i++) {
                ca[i] = csx[j];
            }
            for (var j = 0; j < nBlanks; i++, j++) {
                ca[i] = ' ';
            }
        }
        else {
            if (!this._leadingZeros) {
                for (i = 0; i < nBlanks; i++) {
                    ca[i] = ' ';
                }
                if (neg) {
                    ca[i++] = '-';
                }
                else if (this._leadingSign) {
                    ca[i++] = '+';
                }
                else if (this._leadingSpace) {
                    ca[i++] = ' ';
                }
            }
            else {
                if (neg) {
                    ca[i++] = '-';
                }
                else if (this._leadingSign) {
                    ca[i++] = '+';
                }
                else if (this._leadingSpace) {
                    ca[i++] = ' ';
                }
                for (var j = 0; j < nBlanks; j++, i++) {
                    ca[i] = '0';
                }
            }
            for (var j = 0; j < nLeadingZeros; j++, i++) {
                ca[i] = '0';
            }
            var csx = SystemEx.JSString.stringToChars(sx);
            jFirst = (neg) ? 1 : 0;
            for (var j = jFirst; j < csx.length; j++, i++) {
                ca[i] = csx[j];
            }
        }
        return SystemEx.JSString.charsToString(ca);
    },
    
    _printXFormatInt16: function SystemEx_Interop_CSyntax__conversionSpecification$_printXFormatInt16(x) {
        /// <param name="x" type="Number" integer="true">
        /// </param>
        /// <returns type="String"></returns>
        var sx = null;
        if (x === SystemEx.JSConvert.short_MinValue) {
            sx = '8000';
        }
        else if (x < 0) {
            var t;
            if (x === SystemEx.JSConvert.short_MinValue) {
                t = '0';
            }
            else {
                t = SystemEx.JSConvert.int16ToString(((~(-x - 1)) ^ SystemEx.JSConvert.short_MinValue), 16);
                if (t.charAt(0) === 'F' || t.charAt(0) === 'f') {
                    t = t.substring(16, 32 - 16);
                }
            }
            switch (t.length) {
                case 1:
                    sx = '800' + t;
                    break;
                case 2:
                    sx = '80' + t;
                    break;
                case 3:
                    sx = '8' + t;
                    break;
                case 4:
                    switch (t.charAt(0)) {
                        case '1':
                            sx = '9' + t.substring(1, 4 - 1);
                            break;
                        case '2':
                            sx = 'a' + t.substring(1, 4 - 1);
                            break;
                        case '3':
                            sx = 'b' + t.substring(1, 4 - 1);
                            break;
                        case '4':
                            sx = 'c' + t.substring(1, 4 - 1);
                            break;
                        case '5':
                            sx = 'd' + t.substring(1, 4 - 1);
                            break;
                        case '6':
                            sx = 'e' + t.substring(1, 4 - 1);
                            break;
                        case '7':
                            sx = 'f' + t.substring(1, 4 - 1);
                            break;
                    }
                    break;
            }
        }
        else {
            sx = SystemEx.JSConvert.int32ToString(x, 16);
        }
        return this._printXFormat(sx);
    },
    
    _printXFormatInt64: function SystemEx_Interop_CSyntax__conversionSpecification$_printXFormatInt64(x) {
        /// <param name="x" type="Number" integer="true">
        /// </param>
        /// <returns type="String"></returns>
        var sx = null;
        if (x === SystemEx.JSConvert.long_MinValue) {
            sx = '8000000000000000';
        }
        else if (x < 0) {
            var t = SystemEx.JSConvert.int64ToString((~(-x - 1)) ^ SystemEx.JSConvert.long_MinValue, 16);
            switch (t.length) {
                case 1:
                    sx = '800000000000000' + t;
                    break;
                case 2:
                    sx = '80000000000000' + t;
                    break;
                case 3:
                    sx = '8000000000000' + t;
                    break;
                case 4:
                    sx = '800000000000' + t;
                    break;
                case 5:
                    sx = '80000000000' + t;
                    break;
                case 6:
                    sx = '8000000000' + t;
                    break;
                case 7:
                    sx = '800000000' + t;
                    break;
                case 8:
                    sx = '80000000' + t;
                    break;
                case 9:
                    sx = '8000000' + t;
                    break;
                case 10:
                    sx = '800000' + t;
                    break;
                case 11:
                    sx = '80000' + t;
                    break;
                case 12:
                    sx = '8000' + t;
                    break;
                case 13:
                    sx = '800' + t;
                    break;
                case 14:
                    sx = '80' + t;
                    break;
                case 15:
                    sx = '8' + t;
                    break;
                case 16:
                    switch (t.charAt(0)) {
                        case '1':
                            sx = '9' + t.substring(1, 16 - 1);
                            break;
                        case '2':
                            sx = 'a' + t.substring(1, 16 - 1);
                            break;
                        case '3':
                            sx = 'b' + t.substring(1, 16 - 1);
                            break;
                        case '4':
                            sx = 'c' + t.substring(1, 16 - 1);
                            break;
                        case '5':
                            sx = 'd' + t.substring(1, 16 - 1);
                            break;
                        case '6':
                            sx = 'e' + t.substring(1, 16 - 1);
                            break;
                        case '7':
                            sx = 'f' + t.substring(1, 16 - 1);
                            break;
                    }
                    break;
            }
        }
        else {
            sx = SystemEx.JSConvert.int64ToString(x, 16);
        }
        return this._printXFormat(sx);
    },
    
    _printXFormatInt32: function SystemEx_Interop_CSyntax__conversionSpecification$_printXFormatInt32(x) {
        /// <param name="x" type="Number" integer="true">
        /// </param>
        /// <returns type="String"></returns>
        var sx = null;
        if (x === SystemEx.JSConvert.int_MinValue) {
            sx = '80000000';
        }
        else if (x < 0) {
            var t = SystemEx.JSConvert.int32ToString((~(-x - 1)) ^ SystemEx.JSConvert.int_MinValue, 16);
            switch (t.length) {
                case 1:
                    sx = '8000000' + t;
                    break;
                case 2:
                    sx = '800000' + t;
                    break;
                case 3:
                    sx = '80000' + t;
                    break;
                case 4:
                    sx = '8000' + t;
                    break;
                case 5:
                    sx = '800' + t;
                    break;
                case 6:
                    sx = '80' + t;
                    break;
                case 7:
                    sx = '8' + t;
                    break;
                case 8:
                    switch (t.charAt(0)) {
                        case '1':
                            sx = '9' + t.substring(1, 8 - 1);
                            break;
                        case '2':
                            sx = 'a' + t.substring(1, 8 - 1);
                            break;
                        case '3':
                            sx = 'b' + t.substring(1, 8 - 1);
                            break;
                        case '4':
                            sx = 'c' + t.substring(1, 8 - 1);
                            break;
                        case '5':
                            sx = 'd' + t.substring(1, 8 - 1);
                            break;
                        case '6':
                            sx = 'e' + t.substring(1, 8 - 1);
                            break;
                        case '7':
                            sx = 'f' + t.substring(1, 8 - 1);
                            break;
                    }
                    break;
            }
        }
        else {
            sx = SystemEx.JSConvert.int32ToString(x, 16);
        }
        return this._printXFormat(sx);
    },
    
    _printXFormat: function SystemEx_Interop_CSyntax__conversionSpecification$_printXFormat(sx) {
        /// <param name="sx" type="String">
        /// </param>
        /// <returns type="String"></returns>
        var nLeadingZeros = 0;
        var nBlanks = 0;
        if (sx === '0' && this._precisionSet && this._precision === 0) {
            sx = '';
        }
        if (this._precisionSet) {
            nLeadingZeros = this._precision - sx.length;
        }
        if (nLeadingZeros < 0) {
            nLeadingZeros = 0;
        }
        if (this._fieldWidthSet) {
            nBlanks = this._fieldWidth - nLeadingZeros - sx.length;
            if (this._alternateForm) {
                nBlanks = nBlanks - 2;
            }
        }
        if (nBlanks < 0) {
            nBlanks = 0;
        }
        var n = 0;
        if (this._alternateForm) {
            n += 2;
        }
        n += nLeadingZeros;
        n += sx.length;
        n += nBlanks;
        var ca = new Array(n);
        var i = 0;
        if (this._leftJustify) {
            if (this._alternateForm) {
                ca[i++] = '0';
                ca[i++] = 'x';
            }
            for (var j = 0; j < nLeadingZeros; j++, i++) {
                ca[i] = '0';
            }
            var csx = SystemEx.JSString.stringToChars(sx);
            for (var j = 0; j < csx.length; j++, i++) {
                ca[i] = csx[j];
            }
            for (var j = 0; j < nBlanks; j++, i++) {
                ca[i] = ' ';
            }
        }
        else {
            if (!this._leadingZeros) {
                for (var j = 0; j < nBlanks; j++, i++) {
                    ca[i] = ' ';
                }
            }
            if (this._alternateForm) {
                ca[i++] = '0';
                ca[i++] = 'x';
            }
            if (this._leadingZeros) {
                for (var j = 0; j < nBlanks; j++, i++) {
                    ca[i] = '0';
                }
            }
            for (var j = 0; j < nLeadingZeros; j++, i++) {
                ca[i] = '0';
            }
            var csx = SystemEx.JSString.stringToChars(sx);
            for (var j = 0; j < csx.length; j++, i++) {
                ca[i] = csx[j];
            }
        }
        var caReturn = SystemEx.JSString.charsToString(ca);
        if (this._conversionCharacter === 'X') {
            caReturn = caReturn.toUpperCase();
        }
        return caReturn;
    },
    
    _printOFormatInt16: function SystemEx_Interop_CSyntax__conversionSpecification$_printOFormatInt16(x) {
        /// <param name="x" type="Number" integer="true">
        /// </param>
        /// <returns type="String"></returns>
        var sx = null;
        if (x === SystemEx.JSConvert.short_MinValue) {
            sx = '100000';
        }
        else if (x < 0) {
            var t = SystemEx.JSConvert.int16ToString(((~(-x - 1)) ^ SystemEx.JSConvert.short_MinValue), 8);
            switch (t.length) {
                case 1:
                    sx = '10000' + t;
                    break;
                case 2:
                    sx = '1000' + t;
                    break;
                case 3:
                    sx = '100' + t;
                    break;
                case 4:
                    sx = '10' + t;
                    break;
                case 5:
                    sx = '1' + t;
                    break;
            }
        }
        else {
            sx = SystemEx.JSConvert.int16ToString(x, 8);
        }
        return this._printOFormat(sx);
    },
    
    _printOFormatInt64: function SystemEx_Interop_CSyntax__conversionSpecification$_printOFormatInt64(x) {
        /// <param name="x" type="Number" integer="true">
        /// </param>
        /// <returns type="String"></returns>
        var sx = null;
        if (x === SystemEx.JSConvert.long_MinValue) {
            sx = '1000000000000000000000';
        }
        else if (x < 0) {
            var t = SystemEx.JSConvert.int64ToString((~(-x - 1)) ^ SystemEx.JSConvert.long_MinValue, 8);
            switch (t.length) {
                case 1:
                    sx = '100000000000000000000' + t;
                    break;
                case 2:
                    sx = '10000000000000000000' + t;
                    break;
                case 3:
                    sx = '1000000000000000000' + t;
                    break;
                case 4:
                    sx = '100000000000000000' + t;
                    break;
                case 5:
                    sx = '10000000000000000' + t;
                    break;
                case 6:
                    sx = '1000000000000000' + t;
                    break;
                case 7:
                    sx = '100000000000000' + t;
                    break;
                case 8:
                    sx = '10000000000000' + t;
                    break;
                case 9:
                    sx = '1000000000000' + t;
                    break;
                case 10:
                    sx = '100000000000' + t;
                    break;
                case 11:
                    sx = '10000000000' + t;
                    break;
                case 12:
                    sx = '1000000000' + t;
                    break;
                case 13:
                    sx = '100000000' + t;
                    break;
                case 14:
                    sx = '10000000' + t;
                    break;
                case 15:
                    sx = '1000000' + t;
                    break;
                case 16:
                    sx = '100000' + t;
                    break;
                case 17:
                    sx = '10000' + t;
                    break;
                case 18:
                    sx = '1000' + t;
                    break;
                case 19:
                    sx = '100' + t;
                    break;
                case 20:
                    sx = '10' + t;
                    break;
                case 21:
                    sx = '1' + t;
                    break;
            }
        }
        else {
            sx = SystemEx.JSConvert.int64ToString(x, 8);
        }
        return this._printOFormat(sx);
    },
    
    _printOFormatInt32: function SystemEx_Interop_CSyntax__conversionSpecification$_printOFormatInt32(x) {
        /// <param name="x" type="Number" integer="true">
        /// </param>
        /// <returns type="String"></returns>
        var sx = null;
        if (x === SystemEx.JSConvert.int_MinValue) {
            sx = '20000000000';
        }
        else if (x < 0) {
            var t = SystemEx.JSConvert.int32ToString((~(-x - 1)) ^ SystemEx.JSConvert.int_MinValue, 8);
            switch (t.length) {
                case 1:
                    sx = '2000000000' + t;
                    break;
                case 2:
                    sx = '200000000' + t;
                    break;
                case 3:
                    sx = '20000000' + t;
                    break;
                case 4:
                    sx = '2000000' + t;
                    break;
                case 5:
                    sx = '200000' + t;
                    break;
                case 6:
                    sx = '20000' + t;
                    break;
                case 7:
                    sx = '2000' + t;
                    break;
                case 8:
                    sx = '200' + t;
                    break;
                case 9:
                    sx = '20' + t;
                    break;
                case 10:
                    sx = '2' + t;
                    break;
                case 11:
                    sx = '3' + t.substring(1, t.length);
                    break;
            }
        }
        else {
            sx = SystemEx.JSConvert.int32ToString(x, 8);
        }
        return this._printOFormat(sx);
    },
    
    _printOFormat: function SystemEx_Interop_CSyntax__conversionSpecification$_printOFormat(sx) {
        /// <param name="sx" type="String">
        /// </param>
        /// <returns type="String"></returns>
        var nLeadingZeros = 0;
        var nBlanks = 0;
        if (sx === '0' && this._precisionSet && this._precision === 0) {
            sx = '';
        }
        if (this._precisionSet) {
            nLeadingZeros = this._precision - sx.length;
        }
        if (this._alternateForm) {
            nLeadingZeros++;
        }
        if (nLeadingZeros < 0) {
            nLeadingZeros = 0;
        }
        if (this._fieldWidthSet) {
            nBlanks = this._fieldWidth - nLeadingZeros - sx.length;
        }
        if (nBlanks < 0) {
            nBlanks = 0;
        }
        var n = nLeadingZeros + sx.length + nBlanks;
        var ca = new Array(n);
        var i;
        if (this._leftJustify) {
            for (i = 0; i < nLeadingZeros; i++) {
                ca[i] = '0';
            }
            var csx = SystemEx.JSString.stringToChars(sx);
            for (var j = 0; j < csx.length; j++, i++) {
                ca[i] = csx[j];
            }
            for (var j = 0; j < nBlanks; j++, i++) {
                ca[i] = ' ';
            }
        }
        else {
            if (this._leadingZeros) {
                for (i = 0; i < nBlanks; i++) {
                    ca[i] = '0';
                }
            }
            else {
                for (i = 0; i < nBlanks; i++) {
                    ca[i] = ' ';
                }
            }
            for (var j = 0; j < nLeadingZeros; j++, i++) {
                ca[i] = '0';
            }
            var csx = SystemEx.JSString.stringToChars(sx);
            for (var j = 0; j < csx.length; j++, i++) {
                ca[i] = csx[j];
            }
        }
        return SystemEx.JSString.charsToString(ca);
    },
    
    _printCFormat: function SystemEx_Interop_CSyntax__conversionSpecification$_printCFormat(x) {
        /// <param name="x" type="String">
        /// </param>
        /// <returns type="String"></returns>
        var nPrint = 1;
        var width = this._fieldWidth;
        if (!this._fieldWidthSet) {
            width = nPrint;
        }
        var ca = new Array(width);
        var i = 0;
        if (this._leftJustify) {
            ca[0] = x;
            for (i = 1; i <= width - nPrint; i++) {
                ca[i] = ' ';
            }
        }
        else {
            for (i = 0; i < width - nPrint; i++) {
                ca[i] = ' ';
            }
            ca[i] = x;
        }
        return SystemEx.JSString.charsToString(ca);
    },
    
    _printSFormat: function SystemEx_Interop_CSyntax__conversionSpecification$_printSFormat(x) {
        /// <param name="x" type="String">
        /// </param>
        /// <returns type="String"></returns>
        var nPrint = x.length;
        var width = this._fieldWidth;
        if (this._precisionSet && nPrint > this._precision) {
            nPrint = this._precision;
        }
        if (!this._fieldWidthSet) {
            width = nPrint;
        }
        var n = 0;
        if (width > nPrint) {
            n += width - nPrint;
        }
        if (nPrint >= x.length) {
            n += x.length;
        }
        else {
            n += nPrint;
        }
        var ca = new Array(n);
        var i = 0;
        if (this._leftJustify) {
            if (nPrint >= x.length) {
                var csx = SystemEx.JSString.stringToChars(x);
                for (i = 0; i < x.length; i++) {
                    ca[i] = csx[i];
                }
            }
            else {
                var csx = SystemEx.JSString.stringToChars(x.substring(0, nPrint - 0));
                for (i = 0; i < nPrint; i++) {
                    ca[i] = csx[i];
                }
            }
            for (var j = 0; j < width - nPrint; j++, i++) {
                ca[i] = ' ';
            }
        }
        else {
            for (i = 0; i < width - nPrint; i++) {
                ca[i] = ' ';
            }
            if (nPrint >= x.length) {
                var csx = SystemEx.JSString.stringToChars(x);
                for (var j = 0; j < x.length; i++, j++) {
                    ca[i] = csx[j];
                }
            }
            else {
                var csx = SystemEx.JSString.stringToChars(x.substring(0, nPrint - 0));
                for (var j = 0; j < nPrint; i++, j++) {
                    ca[i] = csx[j];
                }
            }
        }
        return SystemEx.JSString.charsToString(ca);
    },
    
    _setConversionCharacter: function SystemEx_Interop_CSyntax__conversionSpecification$_setConversionCharacter() {
        /// <returns type="Boolean"></returns>
        var ret = false;
        this._conversionCharacter = '\u0000';
        if (this._pos < this._fmt.length) {
            var c = this._fmt.charAt(this._pos);
            if (c === 'i' || c === 'd' || c === 'f' || c === 'g' || c === 'G' || c === 'o' || c === 'x' || c === 'X' || c === 'e' || c === 'E' || c === 'c' || c === 's' || c === '%') {
                this._conversionCharacter = c;
                this._pos++;
                ret = true;
            }
        }
        return ret;
    },
    
    _setOptionalHL: function SystemEx_Interop_CSyntax__conversionSpecification$_setOptionalHL() {
        this._optionalh = false;
        this._optionall = false;
        this._optionalL = false;
        if (this._pos < this._fmt.length) {
            var c = this._fmt.charAt(this._pos);
            if (c === 'h') {
                this._optionalh = true;
                this._pos++;
            }
            else if (c === 'l') {
                this._optionall = true;
                this._pos++;
            }
            else if (c === 'L') {
                this._optionalL = true;
                this._pos++;
            }
        }
    },
    
    _setPrecision: function SystemEx_Interop_CSyntax__conversionSpecification$_setPrecision() {
        var firstPos = this._pos;
        this._precisionSet = false;
        if (this._pos < this._fmt.length && this._fmt.charAt(this._pos) === '.') {
            this._pos++;
            if ((this._pos < this._fmt.length) && (this._fmt.charAt(this._pos) === '*')) {
                this._pos++;
                if (!this._setPrecisionArgPosition()) {
                    this._variablePrecision = true;
                    this._precisionSet = true;
                }
                return;
            }
            else {
                while (this._pos < this._fmt.length) {
                    var c = this._fmt.charAt(this._pos);
                    if (SystemEx.JSConvert.char_IsDigit(c)) {
                        this._pos++;
                    }
                    else {
                        break;
                    }
                }
                if (this._pos > firstPos + 1) {
                    var sz = this._fmt.substring(firstPos + 1, this._pos - firstPos + 1);
                    this._precision = parseInt(sz);
                    this._precisionSet = true;
                }
            }
        }
    },
    
    _setFieldWidth: function SystemEx_Interop_CSyntax__conversionSpecification$_setFieldWidth() {
        var firstPos = this._pos;
        this._fieldWidth = 0;
        this._fieldWidthSet = false;
        if ((this._pos < this._fmt.length) && (this._fmt.charAt(this._pos) === '*')) {
            this._pos++;
            if (!this._setFieldWidthArgPosition()) {
                this._variableFieldWidth = true;
                this._fieldWidthSet = true;
            }
        }
        else {
            while (this._pos < this._fmt.length) {
                var c = this._fmt.charAt(this._pos);
                if (SystemEx.JSConvert.char_IsDigit(c)) {
                    this._pos++;
                }
                else {
                    break;
                }
            }
            if (firstPos < this._pos && firstPos < this._fmt.length) {
                var sz = this._fmt.substring(firstPos, this._pos - firstPos);
                this._fieldWidth = parseInt(sz);
                this._fieldWidthSet = true;
            }
        }
    },
    
    _setArgPosition: function SystemEx_Interop_CSyntax__conversionSpecification$_setArgPosition() {
        var xPos;
        for (xPos = this._pos; xPos < this._fmt.length; xPos++) {
            if (!SystemEx.JSConvert.char_IsDigit(this._fmt.charAt(xPos))) {
                break;
            }
        }
        if (xPos > this._pos && xPos < this._fmt.length) {
            if (this._fmt.charAt(xPos) === '$') {
                this._positionalSpecification = true;
                this._argumentPosition = parseInt(this._fmt.substring(this._pos, xPos - this._pos));
                this._pos = xPos + 1;
            }
        }
    },
    
    _setFieldWidthArgPosition: function SystemEx_Interop_CSyntax__conversionSpecification$_setFieldWidthArgPosition() {
        /// <returns type="Boolean"></returns>
        var ret = false;
        var xPos;
        for (xPos = this._pos; xPos < this._fmt.length; xPos++) {
            if (!SystemEx.JSConvert.char_IsDigit(this._fmt.charAt(xPos))) {
                break;
            }
        }
        if (xPos > this._pos && xPos < this._fmt.length) {
            if (this._fmt.charAt(xPos) === '$') {
                this._positionalFieldWidth = true;
                this._argumentPositionForFieldWidth = parseInt(this._fmt.substring(this._pos, xPos - this._pos));
                this._pos = xPos + 1;
                ret = true;
            }
        }
        return ret;
    },
    
    _setPrecisionArgPosition: function SystemEx_Interop_CSyntax__conversionSpecification$_setPrecisionArgPosition() {
        /// <returns type="Boolean"></returns>
        var ret = false;
        var xPos;
        for (xPos = this._pos; xPos < this._fmt.length; xPos++) {
            if (!SystemEx.JSConvert.char_IsDigit(this._fmt.charAt(xPos))) {
                break;
            }
        }
        if (xPos > this._pos && xPos < this._fmt.length) {
            if (this._fmt.charAt(xPos) === '$') {
                this._positionalPrecision = true;
                this._argumentPositionForPrecision = parseInt(this._fmt.substring(this._pos, xPos - this._pos));
                this._pos = xPos + 1;
                ret = true;
            }
        }
        return ret;
    },
    
    _isPositionalSpecification: function SystemEx_Interop_CSyntax__conversionSpecification$_isPositionalSpecification() {
        /// <returns type="Boolean"></returns>
        return this._positionalSpecification;
    },
    
    _getArgumentPosition: function SystemEx_Interop_CSyntax__conversionSpecification$_getArgumentPosition() {
        /// <returns type="Number" integer="true"></returns>
        return this._argumentPosition;
    },
    
    _isPositionalFieldWidth: function SystemEx_Interop_CSyntax__conversionSpecification$_isPositionalFieldWidth() {
        /// <returns type="Boolean"></returns>
        return this._positionalFieldWidth;
    },
    
    _getArgumentPositionForFieldWidth: function SystemEx_Interop_CSyntax__conversionSpecification$_getArgumentPositionForFieldWidth() {
        /// <returns type="Number" integer="true"></returns>
        return this._argumentPositionForFieldWidth;
    },
    
    _isPositionalPrecision: function SystemEx_Interop_CSyntax__conversionSpecification$_isPositionalPrecision() {
        /// <returns type="Boolean"></returns>
        return this._positionalPrecision;
    },
    
    _getArgumentPositionForPrecision: function SystemEx_Interop_CSyntax__conversionSpecification$_getArgumentPositionForPrecision() {
        /// <returns type="Number" integer="true"></returns>
        return this._argumentPositionForPrecision;
    },
    
    _setFlagCharacters: function SystemEx_Interop_CSyntax__conversionSpecification$_setFlagCharacters() {
        this._thousands = false;
        this._leftJustify = false;
        this._leadingSign = false;
        this._leadingSpace = false;
        this._alternateForm = false;
        this._leadingZeros = false;
        for (; this._pos < this._fmt.length; this._pos++) {
            var c = this._fmt.charAt(this._pos);
            if (c === '\'') {
                this._thousands = true;
            }
            else if (c === '-') {
                this._leftJustify = true;
                this._leadingZeros = false;
            }
            else if (c === '+') {
                this._leadingSign = true;
                this._leadingSpace = false;
            }
            else if (c === ' ') {
                if (!this._leadingSign) {
                    this._leadingSpace = true;
                }
            }
            else if (c === '#') {
                this._alternateForm = true;
            }
            else if (c === '0') {
                if (!this._leftJustify) {
                    this._leadingZeros = true;
                }
            }
            else {
                break;
            }
        }
    }
}


////////////////////////////////////////////////////////////////////////////////
// SystemEx.Interop.CSyntax.PrintfFormat

SystemEx.Interop.CSyntax.PrintfFormat = function SystemEx_Interop_CSyntax_PrintfFormat(fmtArg) {
    /// <param name="fmtArg" type="String">
    /// </param>
    /// <field name="_cPos" type="Number" integer="true">
    /// </field>
    /// <field name="_vFmt" type="Array">
    /// </field>
    this._vFmt = [];
    var ePos = 0;
    var sFmt = null;
    var unCS = this._nonControl(fmtArg, 0);
    if (unCS != null) {
        sFmt = new SystemEx.Interop.CSyntax._conversionSpecification();
        sFmt._setLiteral(unCS);
        this._vFmt.add(sFmt);
    }
    while (this._cPos !== -1 && this._cPos < fmtArg.length) {
        for (ePos = this._cPos + 1; ePos < fmtArg.length; ePos++) {
            var c = '\u0000';
            c = fmtArg.charAt(ePos);
            if (c === 'i') {
                break;
            }
            if (c === 'd') {
                break;
            }
            if (c === 'f') {
                break;
            }
            if (c === 'g') {
                break;
            }
            if (c === 'G') {
                break;
            }
            if (c === 'o') {
                break;
            }
            if (c === 'x') {
                break;
            }
            if (c === 'X') {
                break;
            }
            if (c === 'e') {
                break;
            }
            if (c === 'E') {
                break;
            }
            if (c === 'c') {
                break;
            }
            if (c === 's') {
                break;
            }
            if (c === '%') {
                break;
            }
        }
        ePos = Math.min(ePos + 1, fmtArg.length);
        sFmt = new SystemEx.Interop.CSyntax._conversionSpecification(fmtArg.substring(this._cPos, ePos));
        this._vFmt.add(sFmt);
        unCS = this._nonControl(fmtArg, ePos);
        if (unCS != null) {
            sFmt = new SystemEx.Interop.CSyntax._conversionSpecification();
            sFmt._setLiteral(unCS);
            this._vFmt.add(sFmt);
        }
    }
}
SystemEx.Interop.CSyntax.PrintfFormat.prototype = {
    _cPos: 0,
    
    _nonControl: function SystemEx_Interop_CSyntax_PrintfFormat$_nonControl(s, start) {
        /// <param name="s" type="String">
        /// </param>
        /// <param name="start" type="Number" integer="true">
        /// </param>
        /// <returns type="String"></returns>
        this._cPos = s.indexOf('%', start);
        if (this._cPos === -1) {
            this._cPos = s.length;
        }
        return s.substring(start, this._cPos);
    },
    
    sprintfArray: function SystemEx_Interop_CSyntax_PrintfFormat$sprintfArray(o) {
        /// <param name="o" type="Array" elementType="Object">
        /// </param>
        /// <returns type="String"></returns>
        var c = '\u0000';
        var i = 0;
        var sb = new ss.StringBuilder();
        var $enum1 = ss.IEnumerator.getEnumerator(this._vFmt);
        while ($enum1.moveNext()) {
            var cs = $enum1.get_current();
            c = cs._getConversionCharacter();
            if (c === '\u0000') {
                sb.append(cs._getLiteral());
            }
            else if (c === '%') {
                sb.append('%');
            }
            else {
                if (cs._isPositionalSpecification()) {
                    i = cs._getArgumentPosition() - 1;
                    if (cs._isPositionalFieldWidth()) {
                        var ifw = cs._getArgumentPositionForFieldWidth() - 1;
                        cs._setFieldWidthWithArg((o[ifw]));
                    }
                    if (cs._isPositionalPrecision()) {
                        var ipr = cs._getArgumentPositionForPrecision() - 1;
                        cs._setPrecisionWithArg((o[ipr]));
                    }
                }
                else {
                    if (cs._isVariableFieldWidth()) {
                        cs._setFieldWidthWithArg((o[i]));
                        i++;
                    }
                    if (cs._isVariablePrecision()) {
                        cs._setPrecisionWithArg((o[i]));
                        i++;
                    }
                }
                if (Type.canCast(o[i], Number)) {
                    sb.append(cs._internalsprintfInt32((o[i])));
                }
                else if (Type.canCast(o[i], Number)) {
                    sb.append(cs._internalsprintfInt32((o[i])));
                }
                else if (Type.canCast(o[i], Number)) {
                    sb.append(cs._internalsprintfInt32((o[i])));
                }
                else if (Type.canCast(o[i], Number)) {
                    sb.append(cs._internalsprintfInt64((o[i])));
                }
                else if (Type.canCast(o[i], Number)) {
                    sb.append(cs._internalsprintfDouble((o[i])));
                }
                else if (Type.canCast(o[i], Number)) {
                    sb.append(cs._internalsprintfDouble((o[i])));
                }
                else if (Type.canCast(o[i], String)) {
                    sb.append(cs._internalsprintfInt32((o[i])));
                }
                else if (Type.canCast(o[i], String)) {
                    sb.append(cs._internalsprintfString(o[i]));
                }
                else {
                    sb.append(cs._internalsprintfObject(o[i]));
                }
                if (!cs._isPositionalSpecification()) {
                    i++;
                }
            }
        }
        return sb.toString();
    },
    
    sprintf: function SystemEx_Interop_CSyntax_PrintfFormat$sprintf() {
        /// <returns type="String"></returns>
        var c = '\u0000';
        var sb = new ss.StringBuilder();
        var $enum1 = ss.IEnumerator.getEnumerator(this._vFmt);
        while ($enum1.moveNext()) {
            var cs = $enum1.get_current();
            c = cs._getConversionCharacter();
            if (c === '\u0000') {
                sb.append(cs._getLiteral());
            }
            else if (c === '%') {
                sb.append('%');
            }
        }
        return sb.toString();
    },
    
    sprintfInt32: function SystemEx_Interop_CSyntax_PrintfFormat$sprintfInt32(x) {
        /// <param name="x" type="Number" integer="true">
        /// </param>
        /// <returns type="String"></returns>
        var c = '\u0000';
        var sb = new ss.StringBuilder();
        var $enum1 = ss.IEnumerator.getEnumerator(this._vFmt);
        while ($enum1.moveNext()) {
            var cs = $enum1.get_current();
            c = cs._getConversionCharacter();
            if (c === '\u0000') {
                sb.append(cs._getLiteral());
            }
            else if (c === '%') {
                sb.append('%');
            }
            else {
                sb.append(cs._internalsprintfInt32(x));
            }
        }
        return sb.toString();
    },
    
    sprintfInt64: function SystemEx_Interop_CSyntax_PrintfFormat$sprintfInt64(x) {
        /// <param name="x" type="Number" integer="true">
        /// </param>
        /// <returns type="String"></returns>
        var c = '\u0000';
        var sb = new ss.StringBuilder();
        var $enum1 = ss.IEnumerator.getEnumerator(this._vFmt);
        while ($enum1.moveNext()) {
            var cs = $enum1.get_current();
            c = cs._getConversionCharacter();
            if (c === '\u0000') {
                sb.append(cs._getLiteral());
            }
            else if (c === '%') {
                sb.append('%');
            }
            else {
                sb.append(cs._internalsprintfInt64(x));
            }
        }
        return sb.toString();
    },
    
    sprintfDouble: function SystemEx_Interop_CSyntax_PrintfFormat$sprintfDouble(x) {
        /// <param name="x" type="Number">
        /// </param>
        /// <returns type="String"></returns>
        var c = '\u0000';
        var sb = new ss.StringBuilder();
        var $enum1 = ss.IEnumerator.getEnumerator(this._vFmt);
        while ($enum1.moveNext()) {
            var cs = $enum1.get_current();
            c = cs._getConversionCharacter();
            if (c === '\u0000') {
                sb.append(cs._getLiteral());
            }
            else if (c === '%') {
                sb.append('%');
            }
            else {
                sb.append(cs._internalsprintfDouble(x));
            }
        }
        return sb.toString();
    },
    
    sprintfString: function SystemEx_Interop_CSyntax_PrintfFormat$sprintfString(x) {
        /// <param name="x" type="String">
        /// </param>
        /// <returns type="String"></returns>
        var c = '\u0000';
        var sb = new ss.StringBuilder();
        var $enum1 = ss.IEnumerator.getEnumerator(this._vFmt);
        while ($enum1.moveNext()) {
            var cs = $enum1.get_current();
            c = cs._getConversionCharacter();
            if (c === '\u0000') {
                sb.append(cs._getLiteral());
            }
            else if (c === '%') {
                sb.append('%');
            }
            else {
                sb.append(cs._internalsprintfString(x));
            }
        }
        return sb.toString();
    },
    
    sprintfObject: function SystemEx_Interop_CSyntax_PrintfFormat$sprintfObject(x) {
        /// <param name="x" type="Object">
        /// </param>
        /// <returns type="String"></returns>
        var c = '\u0000';
        var sb = new ss.StringBuilder();
        var $enum1 = ss.IEnumerator.getEnumerator(this._vFmt);
        while ($enum1.moveNext()) {
            var cs = $enum1.get_current();
            c = cs._getConversionCharacter();
            if (c === '\u0000') {
                sb.append(cs._getLiteral());
            }
            else if (c === '%') {
                sb.append('%');
            }
            else {
                if (Type.canCast(x, Number)) {
                    sb.append(cs._internalsprintfInt32((x)));
                }
                else if (Type.canCast(x, Number)) {
                    sb.append(cs._internalsprintfInt32((x)));
                }
                else if (Type.canCast(x, Number)) {
                    sb.append(cs._internalsprintfInt32((x)));
                }
                else if (Type.canCast(x, Number)) {
                    sb.append(cs._internalsprintfInt64((x)));
                }
                else if (Type.canCast(x, Number)) {
                    sb.append(cs._internalsprintfDouble((x)));
                }
                else if (Type.canCast(x, Number)) {
                    sb.append(cs._internalsprintfDouble((x)));
                }
                else if (Type.canCast(x, String)) {
                    sb.append(cs._internalsprintfInt32((x)));
                }
                else if (Type.canCast(x, String)) {
                    sb.append(cs._internalsprintfString(x));
                }
                else {
                    sb.append(cs._internalsprintfObject(x));
                }
            }
        }
        return sb.toString();
    }
}


////////////////////////////////////////////////////////////////////////////////
// SystemEx.Interop.CSyntax.PrintfHelper

SystemEx.Interop.CSyntax.PrintfHelper = function SystemEx_Interop_CSyntax_PrintfHelper() {
}
SystemEx.Interop.CSyntax.PrintfHelper.sprintf = function SystemEx_Interop_CSyntax_PrintfHelper$sprintf(fmt, vargs) {
    /// <param name="fmt" type="String">
    /// </param>
    /// <param name="vargs" type="Array" elementType="Object">
    /// </param>
    /// <returns type="String"></returns>
    return (((vargs == null) || (vargs.length === 0)) ? fmt : new SystemEx.Interop.CSyntax.PrintfFormat(fmt).sprintfArray(vargs));
}


Type.registerNamespace('SystemEx.IO');

////////////////////////////////////////////////////////////////////////////////
// SystemEx.IO.FileAccess

SystemEx.IO.FileAccess = function() { 
    /// <field name="read" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="readWrite" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="write" type="Number" integer="true" static="true">
    /// </field>
};
SystemEx.IO.FileAccess.prototype = {
    read: 1, 
    readWrite: 3, 
    write: 2
}
SystemEx.IO.FileAccess.registerEnum('SystemEx.IO.FileAccess', false);


////////////////////////////////////////////////////////////////////////////////
// SystemEx.IO.FileMode

SystemEx.IO.FileMode = function() { 
    /// <field name="append" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="create" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="createNew" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="open" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="openOrCreate" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="truncate" type="Number" integer="true" static="true">
    /// </field>
};
SystemEx.IO.FileMode.prototype = {
    append: 6, 
    create: 2, 
    createNew: 1, 
    open: 3, 
    openOrCreate: 4, 
    truncate: 5
}
SystemEx.IO.FileMode.registerEnum('SystemEx.IO.FileMode', false);


////////////////////////////////////////////////////////////////////////////////
// SystemEx.IO.MemoryStream

SystemEx.IO.MemoryStream = function SystemEx_IO_MemoryStream(buffer) {
    /// <param name="buffer" type="Array" elementType="Number" elementInteger="true">
    /// </param>
    /// <field name="_count$1" type="Number" integer="true">
    /// </field>
    /// <field name="_buffer$1" type="Array" elementType="Number" elementInteger="true">
    /// </field>
    /// <field name="_position$1" type="Number" integer="true">
    /// </field>
    SystemEx.IO.MemoryStream.initializeBase(this);
    this._buffer$1 = ((buffer == null) ? SystemEx.IO.MemoryStream.makeBuffer(16) : buffer);
}
SystemEx.IO.MemoryStream.makeBuffer = function SystemEx_IO_MemoryStream$makeBuffer(initialSize) {
    /// <param name="initialSize" type="Number" integer="true">
    /// </param>
    /// <returns type="Array" elementType="Number" elementInteger="true"></returns>
    return new Array((initialSize !== 0) ? initialSize : 16);
}
SystemEx.IO.MemoryStream.prototype = {
    _count$1: 0,
    _buffer$1: null,
    _position$1: 0,
    
    getBuffer: function SystemEx_IO_MemoryStream$getBuffer() {
        /// <returns type="Array" elementType="Number" elementInteger="true"></returns>
        return this._buffer$1;
    },
    
    toArray: function SystemEx_IO_MemoryStream$toArray() {
        /// <returns type="Array" elementType="Number" elementInteger="true"></returns>
        var result = new Array(this._count$1);
        SystemEx.JSArrayEx.copy(this._buffer$1, 0, result, 0, this._count$1);
        return result;
    },
    
    get_length: function SystemEx_IO_MemoryStream$get_length() {
        /// <value type="Number" integer="true"></value>
        return this._count$1;
    },
    
    get_position: function SystemEx_IO_MemoryStream$get_position() {
        /// <value type="Number" integer="true"></value>
        return this._position$1;
    },
    
    readByte: function SystemEx_IO_MemoryStream$readByte() {
        /// <returns type="Number" integer="true"></returns>
        return ((this._position$1 < this._buffer$1.length) ? this._buffer$1[this._position$1++] : -1);
    },
    
    writeByte: function SystemEx_IO_MemoryStream$writeByte(b) {
        /// <param name="b" type="Number" integer="true">
        /// </param>
        if (this._buffer$1.length === this._count$1) {
            var newBuf = new Array(this._buffer$1.length * 3 / 2);
            SystemEx.JSArrayEx.copy(this._buffer$1, 0, newBuf, 0, this._count$1);
            this._buffer$1 = newBuf;
        }
        this._buffer$1[this._count$1++] = b;
    },
    
    close: function SystemEx_IO_MemoryStream$close() {
        this._buffer$1 = null;
    },
    
    flush: function SystemEx_IO_MemoryStream$flush() {
    }
}


////////////////////////////////////////////////////////////////////////////////
// SystemEx.IO.SE

SystemEx.IO.SE = function SystemEx_IO_SE() {
}
SystemEx.IO.SE.internalReadByte = function SystemEx_IO_SE$internalReadByte(s) {
    /// <param name="s" type="SystemEx.IO.Stream">
    /// </param>
    /// <returns type="Number" integer="true"></returns>
    var v = s.readByte();
    if (v === -1) {
        throw new Error('IOException: EOF');
    }
    return v;
}
SystemEx.IO.SE._internalReadInt16 = function SystemEx_IO_SE$_internalReadInt16(s) {
    /// <param name="s" type="SystemEx.IO.Stream">
    /// </param>
    /// <returns type="Number" integer="true"></returns>
    var a = s.readByte();
    var b = SystemEx.IO.SE.internalReadByte(s);
    return ((a << 8) | b);
}
SystemEx.IO.SE.readByte = function SystemEx_IO_SE$readByte(s) {
    /// <param name="s" type="SystemEx.IO.Stream">
    /// </param>
    /// <returns type="Number" integer="true"></returns>
    var v = s.readByte();
    if (v === -1) {
        throw new Error('IOException: EOF');
    }
    return v;
}
SystemEx.IO.SE.readBytes = function SystemEx_IO_SE$readBytes(s, b, offset, length) {
    /// <param name="s" type="SystemEx.IO.Stream">
    /// </param>
    /// <param name="b" type="Array" elementType="Number" elementInteger="true">
    /// </param>
    /// <param name="offset" type="Number" integer="true">
    /// </param>
    /// <param name="length" type="Number" integer="true">
    /// </param>
    if ((offset === 0) && (length === 0)) {
        length = b.length;
    }
    while (length > 0) {
        var count = s.read(b, offset, length);
        if (count <= 0) {
            throw new Error('IOException: EOF');
        }
        offset += count;
        length -= count;
    }
}
SystemEx.IO.SE.readBoolean = function SystemEx_IO_SE$readBoolean(s) {
    /// <param name="s" type="SystemEx.IO.Stream">
    /// </param>
    /// <returns type="Boolean"></returns>
    return (SystemEx.IO.SE.readByte(s) !== 0);
}
SystemEx.IO.SE.readChar = function SystemEx_IO_SE$readChar(s) {
    /// <param name="s" type="SystemEx.IO.Stream">
    /// </param>
    /// <returns type="String"></returns>
    var a = s.readByte();
    var b = SystemEx.IO.SE.internalReadByte(s);
    return ((a << 8) | b);
}
SystemEx.IO.SE.readInt16 = function SystemEx_IO_SE$readInt16(s) {
    /// <param name="s" type="SystemEx.IO.Stream">
    /// </param>
    /// <returns type="Number" integer="true"></returns>
    var a = s.readByte();
    var b = SystemEx.IO.SE.internalReadByte(s);
    return ((a << 8) | b);
}
SystemEx.IO.SE.readInt32 = function SystemEx_IO_SE$readInt32(s) {
    /// <param name="s" type="SystemEx.IO.Stream">
    /// </param>
    /// <returns type="Number" integer="true"></returns>
    var a = s.readByte();
    var b = s.readByte();
    var c = s.readByte();
    var d = SystemEx.IO.SE.internalReadByte(s);
    return (a << 24) | (b << 16) | (c << 8) | d;
}
SystemEx.IO.SE.readInt64 = function SystemEx_IO_SE$readInt64(s) {
    /// <param name="s" type="SystemEx.IO.Stream">
    /// </param>
    /// <returns type="Number" integer="true"></returns>
    var a = SystemEx.IO.SE.readInt32(s);
    var b = SystemEx.IO.SE.readInt32(s) & 4294967295;
    return (a << 32) | b;
}
SystemEx.IO.SE.readSingle = function SystemEx_IO_SE$readSingle(s) {
    /// <param name="s" type="SystemEx.IO.Stream">
    /// </param>
    /// <returns type="Number"></returns>
    return SystemEx.JSConvert.int32BitsToSingle(SystemEx.IO.SE.readInt32(s));
}
SystemEx.IO.SE.readDouble = function SystemEx_IO_SE$readDouble(s) {
    /// <param name="s" type="SystemEx.IO.Stream">
    /// </param>
    /// <returns type="Number"></returns>
    throw new Error('NotSupportedException: readDouble');
}
SystemEx.IO.SE.readString = function SystemEx_IO_SE$readString(s) {
    /// <param name="s" type="SystemEx.IO.Stream">
    /// </param>
    /// <returns type="String"></returns>
    var bytes = SystemEx.IO.SE._internalReadInt16(s);
    var b = new ss.StringBuilder();
    while (bytes > 0) {
        bytes -= SystemEx.IO.SE._readUtfChar(s, b);
    }
    return b.toString();
}
SystemEx.IO.SE.readStringLine = function SystemEx_IO_SE$readStringLine(s) {
    /// <param name="s" type="SystemEx.IO.Stream">
    /// </param>
    /// <returns type="String"></returns>
    throw new Error('NotSupportedException: ReadLine');
}
SystemEx.IO.SE._readUtfChar = function SystemEx_IO_SE$_readUtfChar(s, sb) {
    /// <param name="s" type="SystemEx.IO.Stream">
    /// </param>
    /// <param name="sb" type="ss.StringBuilder">
    /// </param>
    /// <returns type="Number" integer="true"></returns>
    var a = SystemEx.IO.SE.internalReadByte(s);
    if ((a & 128) === 0) {
        sb.append(a);
        return 1;
    }
    if ((a & 224) === 176) {
        var b = SystemEx.IO.SE.internalReadByte(s);
        sb.append((((a & 31) << 6) | (b & 63)));
        return 2;
    }
    if ((a & 240) === 224) {
        var b = s.readByte();
        var c = SystemEx.IO.SE.internalReadByte(s);
        sb.append((((a & 15) << 12) | ((b & 63) << 6) | (c & 63)));
        return 3;
    }
    throw new Error('IOException: UTFDataFormat:');
}
SystemEx.IO.SE.writeByte = function SystemEx_IO_SE$writeByte(s, v) {
    /// <param name="s" type="SystemEx.IO.Stream">
    /// </param>
    /// <param name="v" type="Number" integer="true">
    /// </param>
    s.writeByte(v);
}
SystemEx.IO.SE.writeBytes = function SystemEx_IO_SE$writeBytes(s, v) {
    /// <param name="s" type="SystemEx.IO.Stream">
    /// </param>
    /// <param name="v" type="String">
    /// </param>
    var length = v.length;
    for (var index = 0; index < length; index++) {
        s.writeByte(v.charAt(index) & 255);
    }
}
SystemEx.IO.SE.writeBoolean = function SystemEx_IO_SE$writeBoolean(s, v) {
    /// <param name="s" type="SystemEx.IO.Stream">
    /// </param>
    /// <param name="v" type="Boolean">
    /// </param>
    s.writeByte(((v) ? 1 : 0));
}
SystemEx.IO.SE.writeChar = function SystemEx_IO_SE$writeChar(s, v) {
    /// <param name="s" type="SystemEx.IO.Stream">
    /// </param>
    /// <param name="v" type="Number" integer="true">
    /// </param>
    s.writeByte((v >> 8));
    s.writeByte(v);
}
SystemEx.IO.SE.writeInt16 = function SystemEx_IO_SE$writeInt16(s, v) {
    /// <param name="s" type="SystemEx.IO.Stream">
    /// </param>
    /// <param name="v" type="Number" integer="true">
    /// </param>
    s.writeByte((v >> 8));
    s.writeByte(v);
}
SystemEx.IO.SE.writeInt32 = function SystemEx_IO_SE$writeInt32(s, v) {
    /// <param name="s" type="SystemEx.IO.Stream">
    /// </param>
    /// <param name="v" type="Number" integer="true">
    /// </param>
    s.writeByte((v >> 24));
    s.writeByte((v >> 16));
    s.writeByte((v >> 8));
    s.writeByte(v);
}
SystemEx.IO.SE.writeInt64 = function SystemEx_IO_SE$writeInt64(s, v) {
    /// <param name="s" type="SystemEx.IO.Stream">
    /// </param>
    /// <param name="v" type="Number" integer="true">
    /// </param>
    SystemEx.IO.SE.writeInt32(s, (v >> 32));
    SystemEx.IO.SE.writeInt32(s, v);
}
SystemEx.IO.SE.writeSingle = function SystemEx_IO_SE$writeSingle(s, v) {
    /// <param name="s" type="SystemEx.IO.Stream">
    /// </param>
    /// <param name="v" type="Number">
    /// </param>
    SystemEx.IO.SE.writeInt32(s, SystemEx.JSConvert.singleToInt32Bits(v));
}
SystemEx.IO.SE.writeDouble = function SystemEx_IO_SE$writeDouble(s, v) {
    /// <param name="s" type="SystemEx.IO.Stream">
    /// </param>
    /// <param name="v" type="Number">
    /// </param>
    throw new Error('NotSupportedException: writeDouble');
}
SystemEx.IO.SE.writeString = function SystemEx_IO_SE$writeString(s, v) {
    /// <param name="s" type="SystemEx.IO.Stream">
    /// </param>
    /// <param name="v" type="String">
    /// </param>
    var baos = new SystemEx.IO.MemoryStream();
    for (var index = 0; index < v.length; index++) {
        var c = v.charAt(index);
        if ((c > 0) && (c < 80)) {
            baos.writeByte(c);
        }
        else if (c < '\u0800') {
            baos.writeByte((192 | (31 & (c >> 6))));
            baos.writeByte((128 | (63 & c)));
        }
        else {
            baos.writeByte((224 | (15 & (c >> 12))));
            baos.writeByte((128 | (63 & (c >> 6))));
            baos.writeByte((128 | (63 & c)));
        }
    }
    SystemEx.IO.SE.writeInt16(s, baos.get_length());
    s.write(baos.getBuffer(), 0, baos.get_length());
}


////////////////////////////////////////////////////////////////////////////////
// SystemEx.IO.FileInfo

SystemEx.IO.FileInfo = function SystemEx_IO_FileInfo(fileName, parent) {
    /// <param name="fileName" type="String">
    /// </param>
    /// <param name="parent" type="SystemEx.IO.FileInfo">
    /// </param>
    /// <field name="separatorChar" type="String" static="true">
    /// </field>
    /// <field name="separator" type="String" static="true">
    /// </field>
    /// <field name="root" type="SystemEx.IO.FileInfo" static="true">
    /// </field>
    /// <field name="_parent" type="SystemEx.IO.FileInfo">
    /// </field>
    /// <field name="_name" type="String">
    /// </field>
    while (fileName.endsWith(SystemEx.IO.FileInfo.separator) && (fileName.length > 0)) {
        fileName = fileName.substring(0, fileName.length - 1);
    }
    var cut = fileName.lastIndexOf(SystemEx.IO.FileInfo.separatorChar);
    if (cut === -1) {
        this._name = fileName;
    }
    else if (cut === 0) {
        this._name = fileName.substring(cut, fileName.length);
        this._parent = ((this._name === '') ? null : SystemEx.IO.FileInfo.root);
    }
    else {
        this._name = fileName.substring(cut + 1, fileName.length);
        this._parent = new SystemEx.IO.FileInfo(fileName.substring(0, cut));
    }
}
SystemEx.IO.FileInfo.prototype = {
    _parent: null,
    _name: null,
    
    get_name: function SystemEx_IO_FileInfo$get_name() {
        /// <value type="String"></value>
        return this._name;
    },
    
    get__parent: function SystemEx_IO_FileInfo$get__parent() {
        /// <value type="String"></value>
        return ((this._parent == null) ? '' : this._parent.get_path());
    },
    
    get__parentFileInfo: function SystemEx_IO_FileInfo$get__parentFileInfo() {
        /// <value type="SystemEx.IO.FileInfo"></value>
        return this._parent;
    },
    
    get_path: function SystemEx_IO_FileInfo$get_path() {
        /// <value type="String"></value>
        return ((this._parent == null) ? this._name : this._parent.get_path() + SystemEx.IO.FileInfo.separatorChar + this._name);
    },
    
    _isRoot: function SystemEx_IO_FileInfo$_isRoot() {
        /// <returns type="Boolean"></returns>
        return ((this._name === '') && (this._parent == null));
    },
    
    _isAbsolute: function SystemEx_IO_FileInfo$_isAbsolute() {
        /// <returns type="Boolean"></returns>
        if (this._isRoot()) {
            return true;
        }
        if (this._parent == null) {
            return false;
        }
        return this._parent._isAbsolute();
    },
    
    _getAbsolutePath: function SystemEx_IO_FileInfo$_getAbsolutePath() {
        /// <returns type="String"></returns>
        var path = this._getAbsoluteFile().get_path();
        return ((path.length === 0) ? '/' : path);
    },
    
    _getAbsoluteFile: function SystemEx_IO_FileInfo$_getAbsoluteFile() {
        /// <returns type="SystemEx.IO.FileInfo"></returns>
        if (this._isAbsolute()) {
            return this;
        }
        return new SystemEx.IO.FileInfo(this._name, (this._parent == null) ? SystemEx.IO.FileInfo.root : this._parent._getAbsoluteFile());
    },
    
    _getCanonicalPath: function SystemEx_IO_FileInfo$_getCanonicalPath() {
        /// <returns type="String"></returns>
        return this._getCanonicalFile()._getAbsolutePath();
    },
    
    _getCanonicalFile: function SystemEx_IO_FileInfo$_getCanonicalFile() {
        /// <returns type="SystemEx.IO.FileInfo"></returns>
        var cParent = ((this._parent == null) ? null : this._parent._getCanonicalFile());
        if (this._name === '.') {
            return ((cParent == null) ? SystemEx.IO.FileInfo.root : cParent);
        }
        if ((cParent != null) && (cParent._name === '')) {
            cParent = null;
        }
        if (this._name === '..') {
            if (cParent == null) {
                return SystemEx.IO.FileInfo.root;
            }
            if (cParent._parent == null) {
                return SystemEx.IO.FileInfo.root;
            }
            return cParent._parent;
        }
        return new SystemEx.IO.FileInfo(this._name, ((cParent == null) && (this._name !== '')) ? SystemEx.IO.FileInfo.root : cParent);
    },
    
    _exists: function SystemEx_IO_FileInfo$_exists() {
        /// <returns type="Boolean"></returns>
        try {
            return (SystemEx.Html.LocalStorage.getItem(this._getCanonicalPath()) != null);
        }
        catch (e) {
            if (e.message.startsWith('IOException')) {
                return false;
            }
            throw e;
        }
    },
    
    _isFileInfo: function SystemEx_IO_FileInfo$_isFileInfo() {
        /// <returns type="Boolean"></returns>
        try {
            var s = SystemEx.Html.LocalStorage.getItem(this._getCanonicalPath());
            return ((s != null) && !s.startsWith('{'));
        }
        catch (e) {
            if (e.message.startsWith('IOException')) {
                return false;
            }
            throw e;
        }
    },
    
    get_length: function SystemEx_IO_FileInfo$get_length() {
        /// <value type="Number" integer="true"></value>
        try {
            if (!this._exists()) {
                return 0;
            }
            var raf = new SystemEx.IO.FileStream(null, SystemEx.IO.FileMode.append, SystemEx.IO.FileAccess.read, this);
            var length = raf.get_length();
            raf.close();
            return length;
        }
        catch (e) {
            if (e.message.startsWith('IOException')) {
                return 0;
            }
            throw e;
        }
    },
    
    createNewFile: function SystemEx_IO_FileInfo$createNewFile() {
        /// <returns type="Boolean"></returns>
        if (this._exists() || !this._parent._exists()) {
            return false;
        }
        SystemEx.Html.LocalStorage.setItem(this._getCanonicalPath(), window.btoa(''));
        return true;
    },
    
    _delete_: function SystemEx_IO_FileInfo$_delete_() {
        /// <returns type="Boolean"></returns>
        try {
            if (!this._exists()) {
                return false;
            }
            SystemEx.Html.LocalStorage.removeItem(this._getCanonicalPath());
            return true;
        }
        catch (e) {
            if (e.message.startsWith('IOException')) {
                return false;
            }
            throw e;
        }
    },
    
    _makeDirectory: function SystemEx_IO_FileInfo$_makeDirectory() {
        /// <returns type="Boolean"></returns>
        try {
            if ((this._parent != null) && !this._parent._exists()) {
                return false;
            }
            if (this._exists()) {
                return false;
            }
            SystemEx.Html.LocalStorage.setItem(this._getCanonicalPath(), '{}');
            return true;
        }
        catch (e) {
            if (e.message.startsWith('IOException')) {
                return false;
            }
            throw e;
        }
    },
    
    _makeDirectories: function SystemEx_IO_FileInfo$_makeDirectories() {
        /// <returns type="Boolean"></returns>
        if (this._parent != null) {
            this._parent._makeDirectories();
        }
        return this._makeDirectory();
    },
    
    listFiles: function SystemEx_IO_FileInfo$listFiles(predicate) {
        /// <param name="predicate" type="SystemEx.IO.FileInfoSearchPredicate">
        /// </param>
        /// <returns type="ss.IEnumerable"></returns>
        var files = [];
        try {
            var prefix = this._getCanonicalPath();
            if (!prefix.endsWith(SystemEx.IO.FileInfo.separator)) {
                prefix += SystemEx.IO.FileInfo.separatorChar;
            }
            var cut = prefix.length;
            var count = SystemEx.Html.LocalStorage.get_length();
            for (var index = 0; index < count; index++) {
                var key = SystemEx.Html.LocalStorage.key(index);
                if (key.startsWith(prefix) && (key.indexOf(SystemEx.IO.FileInfo.separatorChar, cut) === -1)) {
                    var name = key.substring(cut, key.length);
                    if ((predicate == null) || predicate.invoke(name, this)) {
                        files.add(new SystemEx.IO.FileInfo(name, this));
                    }
                }
            }
        }
        catch ($e1) {
        }
        return files;
    }
}


////////////////////////////////////////////////////////////////////////////////
// SystemEx.IO.FileStream

SystemEx.IO.FileStream = function SystemEx_IO_FileStream(path, fileMode, fileAccess, fileInfo) {
    /// <param name="path" type="String">
    /// </param>
    /// <param name="fileMode" type="SystemEx.IO.FileMode">
    /// </param>
    /// <param name="fileAccess" type="SystemEx.IO.FileAccess">
    /// </param>
    /// <param name="fileInfo" type="SystemEx.IO.FileInfo">
    /// </param>
    /// <field name="_name$1" type="String">
    /// </field>
    /// <field name="_isWriteable$1" type="Boolean">
    /// </field>
    /// <field name="_isDirty$1" type="Boolean">
    /// </field>
    /// <field name="_data$1" type="String">
    /// </field>
    /// <field name="_newDataPosition$1" type="Number" integer="true">
    /// </field>
    /// <field name="_newData$1" type="ss.StringBuilder">
    /// </field>
    /// <field name="_position$1" type="Number" integer="true">
    /// </field>
    /// <field name="_length$1" type="Number" integer="true">
    /// </field>
    SystemEx.IO.FileStream.initializeBase(this);
    if (fileInfo == null) {
        fileInfo = new SystemEx.IO.FileInfo(path);
    }
    this._name$1 = fileInfo._getCanonicalPath();
    if ((fileAccess !== SystemEx.IO.FileAccess.read) && (fileAccess !== SystemEx.IO.FileAccess.readWrite)) {
        throw new Error('IllegalArgumentException: fileAccess');
    }
    this._isWriteable$1 = (fileAccess === SystemEx.IO.FileAccess.readWrite);
    if (fileInfo._exists()) {
        try {
            this._data$1 = window.atob(SystemEx.Html.LocalStorage.getItem(this._name$1));
            this._length$1 = this._data$1.length;
        }
        catch (e) {
            throw ((e.message.startsWith('IOException')) ? new Error('FileNotFoundException:' + e) : e);
        }
    }
    else if (this._isWriteable$1) {
        this._data$1 = '';
        this._isDirty$1 = true;
        try {
            this.flush();
        }
        catch (e) {
            throw ((e.message.startsWith('IOException')) ? new Error('FileNotFoundException:' + e) : e);
        }
    }
    else {
        throw new Error('FileNotFoundException:' + this._name$1);
    }
}
SystemEx.IO.FileStream.prototype = {
    _name$1: null,
    _isWriteable$1: false,
    _isDirty$1: false,
    _data$1: null,
    _newDataPosition$1: 0,
    _newData$1: null,
    _position$1: 0,
    _length$1: 0,
    
    get_filePointer: function SystemEx_IO_FileStream$get_filePointer() {
        /// <value type="Number" integer="true"></value>
        return this._position$1;
    },
    
    seek: function SystemEx_IO_FileStream$seek(position) {
        /// <param name="position" type="Number" integer="true">
        /// </param>
        if (position < 0) {
            throw new Error('IllegalArgumentException:');
        }
        this._position$1 = position;
    },
    
    get_length: function SystemEx_IO_FileStream$get_length() {
        /// <value type="Number" integer="true"></value>
        return this._length$1;
    },
    set_length: function SystemEx_IO_FileStream$set_length(value) {
        /// <value type="Number" integer="true"></value>
        if (this._length$1 !== value) {
            this._consolidate$1();
            if (this._data$1.length > value) {
                this._data$1 = this._data$1.substring(0, value);
                this._length$1 = value;
            }
            else {
                while (this._length$1 < value) {
                    this.writeByte(0);
                }
            }
        }
        return value;
    },
    
    get_poition: function SystemEx_IO_FileStream$get_poition() {
        /// <value type="Number" integer="true"></value>
        return this._position$1;
    },
    
    close: function SystemEx_IO_FileStream$close() {
        if (this._data$1 != null) {
            this.flush();
            this._data$1 = null;
        }
    },
    
    _consolidate$1: function SystemEx_IO_FileStream$_consolidate$1() {
        if (this._newData$1 == null) {
            return;
        }
        if (this._data$1.length < this._newDataPosition$1) {
            var filler = new ss.StringBuilder();
            while (this._data$1.length + SystemEx.StringBuilderEx.getLength(filler) < this._newDataPosition$1) {
                filler.append('\u0000');
            }
            this._data$1 += filler.toString();
        }
        var p2 = this._newDataPosition$1 + SystemEx.StringBuilderEx.getLength(this._newData$1);
        this._data$1 = this._data$1.substring(0, this._newDataPosition$1) + this._newData$1.toString() + ((p2 < this._data$1.length) ? this._data$1.substring(p2, this._data$1.length) : '');
        this._newData$1 = null;
    },
    
    flush: function SystemEx_IO_FileStream$flush() {
        if (!this._isDirty$1) {
            return;
        }
        this._consolidate$1();
        SystemEx.Html.LocalStorage.setItem(this._name$1, window.btoa(this._data$1));
        this._isDirty$1 = false;
    },
    
    readByte: function SystemEx_IO_FileStream$readByte() {
        /// <returns type="Number" integer="true"></returns>
        if (this._position$1 >= this._length$1) {
            return -1;
        }
        else {
            this._consolidate$1();
            return this._data$1.charAt(this._position$1++);
        }
    },
    
    writeByte: function SystemEx_IO_FileStream$writeByte(b) {
        /// <param name="b" type="Number" integer="true">
        /// </param>
        if (!this._isWriteable$1) {
            throw new Error('IOException: not writeable');
        }
        if (this._newData$1 == null) {
            this._newDataPosition$1 = this._position$1;
            this._newData$1 = new ss.StringBuilder();
        }
        else if (this._newDataPosition$1 + SystemEx.StringBuilderEx.getLength(this._newData$1) !== this._position$1) {
            this._consolidate$1();
            this._newDataPosition$1 = this._position$1;
            this._newData$1 = new ss.StringBuilder();
        }
        this._newData$1.append((b & 255));
        this._position$1++;
        this._length$1 = Math.max(this._position$1, this._length$1);
        this._isDirty$1 = true;
    }
}


////////////////////////////////////////////////////////////////////////////////
// SystemEx.IO.Path

SystemEx.IO.Path = function SystemEx_IO_Path() {
}


////////////////////////////////////////////////////////////////////////////////
// SystemEx.IO.Directory

SystemEx.IO.Directory = function SystemEx_IO_Directory() {
}
SystemEx.IO.Directory.exists = function SystemEx_IO_Directory$exists(path) {
    /// <param name="path" type="String">
    /// </param>
    /// <returns type="Boolean"></returns>
    if (path == null) {
        throw new Error('ArgumentNullException: path');
    }
    return new SystemEx.IO.FileInfo(path)._exists();
}


////////////////////////////////////////////////////////////////////////////////
// SystemEx.IO.File

SystemEx.IO.File = function SystemEx_IO_File() {
}
SystemEx.IO.File.delete_ = function SystemEx_IO_File$delete_(path) {
    /// <param name="path" type="String">
    /// </param>
    /// <returns type="Boolean"></returns>
    if (path == null) {
        throw new Error('ArgumentNullException: path');
    }
    return new SystemEx.IO.FileInfo(path)._delete_();
}
SystemEx.IO.File.exists = function SystemEx_IO_File$exists(path) {
    /// <param name="path" type="String">
    /// </param>
    /// <returns type="Boolean"></returns>
    if (path == null) {
        throw new Error('ArgumentNullException: path');
    }
    return new SystemEx.IO.FileInfo(path)._exists();
}


////////////////////////////////////////////////////////////////////////////////
// SystemEx.IO.Stream

SystemEx.IO.Stream = function SystemEx_IO_Stream() {
}
SystemEx.IO.Stream.prototype = {
    
    read: function SystemEx_IO_Stream$read(b, offset, length) {
        /// <param name="b" type="Array" elementType="Number" elementInteger="true">
        /// </param>
        /// <param name="offset" type="Number" integer="true">
        /// </param>
        /// <param name="length" type="Number" integer="true">
        /// </param>
        /// <returns type="Number" integer="true"></returns>
        if ((offset === 0) && (length === 0)) {
            length = b.length;
        }
        var end = offset + length;
        for (var index = offset; index < end; index++) {
            var r = this.readByte();
            if (r === -1) {
                return ((index === offset) ? -1 : index - offset);
            }
            b[index] = r;
        }
        return length;
    },
    
    write: function SystemEx_IO_Stream$write(b, offset, length) {
        /// <param name="b" type="Array" elementType="Number" elementInteger="true">
        /// </param>
        /// <param name="offset" type="Number" integer="true">
        /// </param>
        /// <param name="length" type="Number" integer="true">
        /// </param>
        if ((offset === 0) && (length === 0)) {
            length = b.length;
        }
        var end = offset + length;
        for (var index = offset; index < end; index++) {
            this.writeByte(b[index]);
        }
    },
    
    dispose: function SystemEx_IO_Stream$dispose() {
        this.close();
    }
}


Type.registerNamespace('SystemEx.Security.Cryptography');

////////////////////////////////////////////////////////////////////////////////
// SystemEx.Security.Cryptography.CrcSlim

SystemEx.Security.Cryptography.CrcSlim = function SystemEx_Security_Cryptography_CrcSlim() {
    /// <field name="_crC_INIT_VALUE" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="_crctable" type="Array" elementType="Number" elementInteger="true" static="true">
    /// </field>
    /// <field name="_chktbl" type="Array" elementType="Number" elementInteger="true" static="true">
    /// </field>
    /// <field name="_chkb" type="Array" elementType="Number" elementInteger="true" static="true">
    /// </field>
}
SystemEx.Security.Cryptography.CrcSlim.crC_Block = function SystemEx_Security_Cryptography_CrcSlim$crC_Block(start, count) {
    /// <param name="start" type="Array" elementType="Number" elementInteger="true">
    /// </param>
    /// <param name="count" type="Number" integer="true">
    /// </param>
    /// <returns type="Number" integer="true"></returns>
    var crc = SystemEx.Security.Cryptography.CrcSlim._crC_INIT_VALUE;
    var ndx = 0;
    while (count-- > 0) {
        crc = ((crc << 8) ^ SystemEx.Security.Cryptography.CrcSlim._crctable[255 & ((crc >>> 8) ^ start[ndx++])]);
    }
    return crc & 65535;
}
SystemEx.Security.Cryptography.CrcSlim.blockSequenceCRCByte = function SystemEx_Security_Cryptography_CrcSlim$blockSequenceCRCByte(b, offset, length, sequence) {
    /// <param name="b" type="Array" elementType="Number" elementInteger="true">
    /// </param>
    /// <param name="offset" type="Number" integer="true">
    /// </param>
    /// <param name="length" type="Number" integer="true">
    /// </param>
    /// <param name="sequence" type="Number" integer="true">
    /// </param>
    /// <returns type="Number" integer="true"></returns>
    if (sequence < 0) {
        throw new Error('InvalidOperationException: sequence < 0, this shouldn\'t happen\n');
    }
    var p_ndx = (sequence % (1024 - 4));
    length = Math.min(60, length);
    SystemEx.JSArrayEx.copy(b, offset, SystemEx.Security.Cryptography.CrcSlim._chkb, 0, length);
    SystemEx.Security.Cryptography.CrcSlim._chkb[length] = SystemEx.Security.Cryptography.CrcSlim._chktbl[p_ndx + 0];
    SystemEx.Security.Cryptography.CrcSlim._chkb[length + 1] = SystemEx.Security.Cryptography.CrcSlim._chktbl[p_ndx + 1];
    SystemEx.Security.Cryptography.CrcSlim._chkb[length + 2] = SystemEx.Security.Cryptography.CrcSlim._chktbl[p_ndx + 2];
    SystemEx.Security.Cryptography.CrcSlim._chkb[length + 3] = SystemEx.Security.Cryptography.CrcSlim._chktbl[p_ndx + 3];
    length += 4;
    var crc = SystemEx.Security.Cryptography.CrcSlim.crC_Block(SystemEx.Security.Cryptography.CrcSlim._chkb, length);
    var x = 0;
    for (var n = 0; n < length; n++) {
        x += SystemEx.Security.Cryptography.CrcSlim._chkb[n] & 255;
    }
    crc ^= x;
    return (crc & 255);
}


////////////////////////////////////////////////////////////////////////////////
// SystemEx.Security.Cryptography.Md4Slim

SystemEx.Security.Cryptography.Md4Slim = function SystemEx_Security_Cryptography_Md4Slim() {
    /// <field name="_blocK_LENGTH" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="_application" type="Array" elementType="Number" elementInteger="true">
    /// </field>
    /// <field name="_count" type="Number" integer="true">
    /// </field>
    /// <field name="_buffer" type="Array" elementType="Number" elementInteger="true">
    /// </field>
    /// <field name="_x" type="Array" elementType="Number" elementInteger="true">
    /// </field>
    this._application = new Array(4);
    this._buffer = new Array(SystemEx.Security.Cryptography.Md4Slim._blocK_LENGTH);
    this._x = new Array(16);
    this.reset();
}
SystemEx.Security.Cryptography.Md4Slim.com_BlockChecksum = function SystemEx_Security_Cryptography_Md4Slim$com_BlockChecksum(buffer, length) {
    /// <param name="buffer" type="Array" elementType="Number" elementInteger="true">
    /// </param>
    /// <param name="length" type="Number" integer="true">
    /// </param>
    /// <returns type="Number" integer="true"></returns>
    var val;
    var md4 = new SystemEx.Security.Cryptography.Md4Slim();
    md4.update(buffer, 0, length);
    var data = md4.getDigest();
    var b = new SystemEx.IO.MemoryStream(data);
    val = SystemEx.IO.SE.readInt32(b) ^ SystemEx.IO.SE.readInt32(b) ^ SystemEx.IO.SE.readInt32(b) ^ SystemEx.IO.SE.readInt32(b);
    return val;
}
SystemEx.Security.Cryptography.Md4Slim.prototype = {
    _count: 0,
    
    reset: function SystemEx_Security_Cryptography_Md4Slim$reset() {
        this._application[0] = 1732584193;
        this._application[1] = 4023233417;
        this._application[2] = 2562383102;
        this._application[3] = 271733878;
        this._count = 0;
        for (var i = 0; i < SystemEx.Security.Cryptography.Md4Slim._blocK_LENGTH; i++) {
            this._buffer[i] = 0;
        }
    },
    
    beginUpdate: function SystemEx_Security_Cryptography_Md4Slim$beginUpdate(b) {
        /// <param name="b" type="Number" integer="true">
        /// </param>
        var i = (this._count % SystemEx.Security.Cryptography.Md4Slim._blocK_LENGTH);
        this._count++;
        this._buffer[i] = b;
        if (i === SystemEx.Security.Cryptography.Md4Slim._blocK_LENGTH - 1) {
            this._transform(this._buffer, 0);
        }
    },
    
    update: function SystemEx_Security_Cryptography_Md4Slim$update(input, offset, len) {
        /// <param name="input" type="Array" elementType="Number" elementInteger="true">
        /// </param>
        /// <param name="offset" type="Number" integer="true">
        /// </param>
        /// <param name="len" type="Number" integer="true">
        /// </param>
        if (offset < 0 || len < 0 || offset + len > input.length) {
            throw new Error('IndexOutOfRangeException:');
        }
        var bufferNdx = (this._count % SystemEx.Security.Cryptography.Md4Slim._blocK_LENGTH);
        this._count += len;
        var partLen = SystemEx.Security.Cryptography.Md4Slim._blocK_LENGTH - bufferNdx;
        var i = 0;
        if (len >= partLen) {
            SystemEx.JSArrayEx.copy(input, offset, this._buffer, bufferNdx, partLen);
            this._transform(this._buffer, 0);
            for (i = partLen; i + SystemEx.Security.Cryptography.Md4Slim._blocK_LENGTH - 1 < len; i += SystemEx.Security.Cryptography.Md4Slim._blocK_LENGTH) {
                this._transform(input, offset + i);
            }
            bufferNdx = 0;
        }
        if (i < len) {
            SystemEx.JSArrayEx.copy(input, offset + i, this._buffer, bufferNdx, len - i);
        }
    },
    
    getDigest: function SystemEx_Security_Cryptography_Md4Slim$getDigest() {
        /// <returns type="Array" elementType="Number" elementInteger="true"></returns>
        var bufferNdx = (this._count % SystemEx.Security.Cryptography.Md4Slim._blocK_LENGTH);
        var padLen = (bufferNdx < 56) ? (56 - bufferNdx) : (120 - bufferNdx);
        var tail = new Array(padLen + 8);
        tail[0] = 128;
        for (var i = 0; i < 8; i++) {
            tail[padLen + i] = ((this._count * 8) >> (8 * i));
        }
        this.update(tail, 0, tail.length);
        var result = new Array(16);
        for (var i = 0; i < 4; i++) {
            for (var j = 0; j < 4; j++) {
                result[i * 4 + j] = (this._application[i] >>> (8 * j));
            }
        }
        this.reset();
        return result;
    },
    
    _transform: function SystemEx_Security_Cryptography_Md4Slim$_transform(block, offset) {
        /// <param name="block" type="Array" elementType="Number" elementInteger="true">
        /// </param>
        /// <param name="offset" type="Number" integer="true">
        /// </param>
        for (var i = 0; i < 16; i++) {
            this._x[i] = ((block[offset++] & 255) | ((block[offset++] & 255) << 8) | ((block[offset++] & 255) << 16) | ((block[offset++] & 255) << 24));
        }
        var A = this._application[0];
        var B = this._application[1];
        var C = this._application[2];
        var D = this._application[3];
        A = this._FF(A, B, C, D, this._x[0], 3);
        D = this._FF(D, A, B, C, this._x[1], 7);
        C = this._FF(C, D, A, B, this._x[2], 11);
        B = this._FF(B, C, D, A, this._x[3], 19);
        A = this._FF(A, B, C, D, this._x[4], 3);
        D = this._FF(D, A, B, C, this._x[5], 7);
        C = this._FF(C, D, A, B, this._x[6], 11);
        B = this._FF(B, C, D, A, this._x[7], 19);
        A = this._FF(A, B, C, D, this._x[8], 3);
        D = this._FF(D, A, B, C, this._x[9], 7);
        C = this._FF(C, D, A, B, this._x[10], 11);
        B = this._FF(B, C, D, A, this._x[11], 19);
        A = this._FF(A, B, C, D, this._x[12], 3);
        D = this._FF(D, A, B, C, this._x[13], 7);
        C = this._FF(C, D, A, B, this._x[14], 11);
        B = this._FF(B, C, D, A, this._x[15], 19);
        A = this._GG(A, B, C, D, this._x[0], 3);
        D = this._GG(D, A, B, C, this._x[4], 5);
        C = this._GG(C, D, A, B, this._x[8], 9);
        B = this._GG(B, C, D, A, this._x[12], 13);
        A = this._GG(A, B, C, D, this._x[1], 3);
        D = this._GG(D, A, B, C, this._x[5], 5);
        C = this._GG(C, D, A, B, this._x[9], 9);
        B = this._GG(B, C, D, A, this._x[13], 13);
        A = this._GG(A, B, C, D, this._x[2], 3);
        D = this._GG(D, A, B, C, this._x[6], 5);
        C = this._GG(C, D, A, B, this._x[10], 9);
        B = this._GG(B, C, D, A, this._x[14], 13);
        A = this._GG(A, B, C, D, this._x[3], 3);
        D = this._GG(D, A, B, C, this._x[7], 5);
        C = this._GG(C, D, A, B, this._x[11], 9);
        B = this._GG(B, C, D, A, this._x[15], 13);
        A = this._HH(A, B, C, D, this._x[0], 3);
        D = this._HH(D, A, B, C, this._x[8], 9);
        C = this._HH(C, D, A, B, this._x[4], 11);
        B = this._HH(B, C, D, A, this._x[12], 15);
        A = this._HH(A, B, C, D, this._x[2], 3);
        D = this._HH(D, A, B, C, this._x[10], 9);
        C = this._HH(C, D, A, B, this._x[6], 11);
        B = this._HH(B, C, D, A, this._x[14], 15);
        A = this._HH(A, B, C, D, this._x[1], 3);
        D = this._HH(D, A, B, C, this._x[9], 9);
        C = this._HH(C, D, A, B, this._x[5], 11);
        B = this._HH(B, C, D, A, this._x[13], 15);
        A = this._HH(A, B, C, D, this._x[3], 3);
        D = this._HH(D, A, B, C, this._x[11], 9);
        C = this._HH(C, D, A, B, this._x[7], 11);
        B = this._HH(B, C, D, A, this._x[15], 15);
        this._application[0] += A;
        this._application[1] += B;
        this._application[2] += C;
        this._application[3] += D;
    },
    
    _FF: function SystemEx_Security_Cryptography_Md4Slim$_FF(a, b, c, d, x, s) {
        /// <param name="a" type="Number" integer="true">
        /// </param>
        /// <param name="b" type="Number" integer="true">
        /// </param>
        /// <param name="c" type="Number" integer="true">
        /// </param>
        /// <param name="d" type="Number" integer="true">
        /// </param>
        /// <param name="x" type="Number" integer="true">
        /// </param>
        /// <param name="s" type="Number" integer="true">
        /// </param>
        /// <returns type="Number" integer="true"></returns>
        var t = a + ((b & c) | (~b & d)) + x;
        return t << s | t >>> (32 - s);
    },
    
    _GG: function SystemEx_Security_Cryptography_Md4Slim$_GG(a, b, c, d, x, s) {
        /// <param name="a" type="Number" integer="true">
        /// </param>
        /// <param name="b" type="Number" integer="true">
        /// </param>
        /// <param name="c" type="Number" integer="true">
        /// </param>
        /// <param name="d" type="Number" integer="true">
        /// </param>
        /// <param name="x" type="Number" integer="true">
        /// </param>
        /// <param name="s" type="Number" integer="true">
        /// </param>
        /// <returns type="Number" integer="true"></returns>
        var t = a + ((b & (c | d)) | (c & d)) + x + 1518500249;
        return t << s | t >>> (32 - s);
    },
    
    _HH: function SystemEx_Security_Cryptography_Md4Slim$_HH(a, b, c, d, x, s) {
        /// <param name="a" type="Number" integer="true">
        /// </param>
        /// <param name="b" type="Number" integer="true">
        /// </param>
        /// <param name="c" type="Number" integer="true">
        /// </param>
        /// <param name="d" type="Number" integer="true">
        /// </param>
        /// <param name="x" type="Number" integer="true">
        /// </param>
        /// <param name="s" type="Number" integer="true">
        /// </param>
        /// <returns type="Number" integer="true"></returns>
        var t = a + (b ^ c ^ d) + x + 1859775393;
        return t << s | t >>> (32 - s);
    }
}


Type.registerNamespace('SystemEx.Text');

////////////////////////////////////////////////////////////////////////////////
// SystemEx.Text.PackText

SystemEx.Text.PackText = function SystemEx_Text_PackText() {
    /// <field name="maX_INFO_KEY" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="maX_INFO_VALUE" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="maX_INFO_STRING" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="SPACES" type="String" static="true">
    /// </field>
    /// <field name="_errorHandler" type="SystemEx.ErrorHandler" static="true">
    /// </field>
}
SystemEx.Text.PackText.get_errorHandler = function SystemEx_Text_PackText$get_errorHandler() {
    /// <value type="SystemEx.ErrorHandler"></value>
    return SystemEx.Text.PackText._errorHandler;
}
SystemEx.Text.PackText.set_errorHandler = function SystemEx_Text_PackText$set_errorHandler(value) {
    /// <value type="SystemEx.ErrorHandler"></value>
    SystemEx.Text.PackText._errorHandler = value;
    return value;
}
SystemEx.Text.PackText.info_GetValueForKey = function SystemEx_Text_PackText$info_GetValueForKey(s, key) {
    /// <summary>
    /// Info_ValueForKey
    /// Searches the string for the given key and returns the associated value, or an empty string.
    /// </summary>
    /// <param name="s" type="String">
    /// </param>
    /// <param name="key" type="String">
    /// </param>
    /// <returns type="String"></returns>
    var tokens = s.split('\\');
    for (var tokenIndex = 0; tokenIndex < tokens.length; tokenIndex += 2) {
        var key1 = tokens[tokenIndex];
        if (tokenIndex + 1 >= tokens.length) {
            if (SystemEx.Text.PackText._errorHandler != null) {
                SystemEx.Text.PackText._errorHandler.invoke(SystemEx.ErrorCode.INFO, 'MISSING VALUE\n', null);
            }
            return s;
        }
        var value1 = tokens[tokenIndex + 1];
        if (key === key1) {
            return value1;
        }
    }
    return String.Empty;
}
SystemEx.Text.PackText.info_RemoveKey = function SystemEx_Text_PackText$info_RemoveKey(s, key) {
    /// <param name="s" type="String">
    /// </param>
    /// <param name="key" type="String">
    /// </param>
    /// <returns type="String"></returns>
    var b = new ss.StringBuilder();
    if (key.indexOf('\\') !== -1) {
        if (SystemEx.Text.PackText._errorHandler != null) {
            SystemEx.Text.PackText._errorHandler.invoke(SystemEx.ErrorCode.INFO, 'Can\'t use a key with a \\\n', null);
        }
        return s;
    }
    var tokens = s.split('\\');
    for (var tokenIndex = 0; tokenIndex < tokens.length; tokenIndex += 2) {
        var key1 = tokens[tokenIndex];
        if (tokenIndex + 1 >= tokens.length) {
            if (SystemEx.Text.PackText._errorHandler != null) {
                SystemEx.Text.PackText._errorHandler.invoke(SystemEx.ErrorCode.INFO, 'MISSING VALUE\n', null);
            }
            return s;
        }
        var value1 = tokens[tokenIndex + 1];
        if (key !== key1) {
            b.append('\\').append(key1).append('\\').append(value1);
        }
    }
    return b.toString();
}
SystemEx.Text.PackText.info_Validate = function SystemEx_Text_PackText$info_Validate(s) {
    /// <summary>
    /// Info_Validate
    /// Some characters are illegal in info strings because they can mess up the server's parsing
    /// </summary>
    /// <param name="s" type="String">
    /// </param>
    /// <returns type="Boolean"></returns>
    return !((s.indexOf('\"') !== -1) || (s.indexOf(';') !== -1));
}
SystemEx.Text.PackText.info_SetValueForKey = function SystemEx_Text_PackText$info_SetValueForKey(s, key, value) {
    /// <param name="s" type="String">
    /// </param>
    /// <param name="key" type="String">
    /// </param>
    /// <param name="value" type="String">
    /// </param>
    /// <returns type="String"></returns>
    if ((value == null) || (value.length === 0)) {
        return s;
    }
    if ((key.indexOf('\\') !== -1) || (value.indexOf('\\') !== -1)) {
        if (SystemEx.Text.PackText._errorHandler != null) {
            SystemEx.Text.PackText._errorHandler.invoke(SystemEx.ErrorCode.INFO, 'Can\'t use keys or values with a \\\n', null);
        }
        return s;
    }
    if (key.indexOf(';') !== -1) {
        if (SystemEx.Text.PackText._errorHandler != null) {
            SystemEx.Text.PackText._errorHandler.invoke(SystemEx.ErrorCode.INFO, 'Can\'t use keys or values with a semicolon\n', null);
        }
        return s;
    }
    if ((key.indexOf('\"') !== -1) || (value.indexOf('\"') !== -1)) {
        if (SystemEx.Text.PackText._errorHandler != null) {
            SystemEx.Text.PackText._errorHandler.invoke(SystemEx.ErrorCode.INFO, 'Can\'t use keys or values with a \"\n', null);
        }
        return s;
    }
    if ((key.length > SystemEx.Text.PackText.maX_INFO_KEY - 1) || (value.length > SystemEx.Text.PackText.maX_INFO_KEY - 1)) {
        if (SystemEx.Text.PackText._errorHandler != null) {
            SystemEx.Text.PackText._errorHandler.invoke(SystemEx.ErrorCode.INFO, 'Keys and values must be < 64 characters.\n', null);
        }
        return s;
    }
    var b = new ss.StringBuilder(SystemEx.Text.PackText.info_RemoveKey(s, key));
    if ((SystemEx.StringBuilderEx.getLength(b) + 2 + key.length + value.length) > SystemEx.Text.PackText.maX_INFO_STRING) {
        if (SystemEx.Text.PackText._errorHandler != null) {
            SystemEx.Text.PackText._errorHandler.invoke(SystemEx.ErrorCode.INFO, 'Info string length exceeded\n', null);
        }
        return s;
    }
    b.append('\\').append(key).append('\\').append(value);
    return b.toString();
}
SystemEx.Text.PackText.info_Print = function SystemEx_Text_PackText$info_Print(s) {
    /// <param name="s" type="String">
    /// </param>
    var b = new ss.StringBuilder();
    var tokens = s.split('\\');
    for (var tokenIndex = 0; tokenIndex < tokens.length; tokenIndex += 2) {
        var key1 = tokens[tokenIndex];
        if ((tokenIndex + 1) >= tokens.length) {
            if (SystemEx.Text.PackText._errorHandler != null) {
                SystemEx.Text.PackText._errorHandler.invoke(SystemEx.ErrorCode.INFO, 'MISSING VALUE\n', null);
            }
            return;
        }
        var value1 = tokens[tokenIndex + 1];
        b.append(key1);
        var length = key1.length;
        if (length < 20) {
            b.append(SystemEx.Text.PackText.SPACES.substring(length, SystemEx.Text.PackText.SPACES.length));
        }
        b.append('=').append(value1).append('\n');
    }
    if (SystemEx.Text.PackText._errorHandler != null) {
        SystemEx.Text.PackText._errorHandler.invoke(SystemEx.ErrorCode.INFO, b.toString(), null);
    }
}


SystemEx.Math3D.registerClass('SystemEx.Math3D');
SystemEx.Plane3.registerClass('SystemEx.Plane3');
SystemEx.ByteBuilder.registerClass('SystemEx.ByteBuilder');
SystemEx.JSSystem.registerClass('SystemEx.JSSystem');
SystemEx.JSString.registerClass('SystemEx.JSString');
SystemEx.JSArrayEx.registerClass('SystemEx.JSArrayEx');
SystemEx.JSConvert.registerClass('SystemEx.JSConvert');
SystemEx.StringBuilderEx.registerClass('SystemEx.StringBuilderEx');
SystemEx.Html.CloseEventArgs.registerClass('SystemEx.Html.CloseEventArgs');
SystemEx.Html.MessageEventArgs.registerClass('SystemEx.Html.MessageEventArgs');
SystemEx.Html.WebSocket.registerClass('SystemEx.Html.WebSocket');
SystemEx.Html.LocalStorage.registerClass('SystemEx.Html.LocalStorage');
SystemEx.Interop.CSyntax._conversionSpecification.registerClass('SystemEx.Interop.CSyntax._conversionSpecification');
SystemEx.Interop.CSyntax.PrintfFormat.registerClass('SystemEx.Interop.CSyntax.PrintfFormat');
SystemEx.Interop.CSyntax.PrintfHelper.registerClass('SystemEx.Interop.CSyntax.PrintfHelper');
SystemEx.IO.Stream.registerClass('SystemEx.IO.Stream', null, ss.IDisposable);
SystemEx.IO.MemoryStream.registerClass('SystemEx.IO.MemoryStream', SystemEx.IO.Stream);
SystemEx.IO.SE.registerClass('SystemEx.IO.SE');
SystemEx.IO.FileInfo.registerClass('SystemEx.IO.FileInfo');
SystemEx.IO.FileStream.registerClass('SystemEx.IO.FileStream', SystemEx.IO.Stream);
SystemEx.IO.Path.registerClass('SystemEx.IO.Path');
SystemEx.IO.Directory.registerClass('SystemEx.IO.Directory');
SystemEx.IO.File.registerClass('SystemEx.IO.File');
SystemEx.Security.Cryptography.CrcSlim.registerClass('SystemEx.Security.Cryptography.CrcSlim');
SystemEx.Security.Cryptography.Md4Slim.registerClass('SystemEx.Security.Cryptography.Md4Slim');
SystemEx.Text.PackText.registerClass('SystemEx.Text.PackText');
SystemEx.Math3D.PITCH = 0;
SystemEx.Math3D.YAW = 1;
SystemEx.Math3D.ROLL = 2;
SystemEx.Math3D._shortratio = 360 / 65536;
SystemEx.Math3D._piratio = (Math.PI / 360);
SystemEx.Math3D._errorHandler = null;
SystemEx.Math3D._m = [ new Array(3), new Array(3), new Array(3) ];
SystemEx.Math3D._im = [ new Array(3), new Array(3), new Array(3) ];
SystemEx.Math3D._tmpmat = [ new Array(3), new Array(3), new Array(3) ];
SystemEx.Math3D._zrot = [ new Array(3), new Array(3), new Array(3) ];
SystemEx.Math3D._vr = [ 0, 0, 0 ];
SystemEx.Math3D._vup = [ 0, 0, 0 ];
SystemEx.Math3D._vf = [ 0, 0, 0 ];
SystemEx.Math3D._planE_XYZ = [ [ 1, 0, 0 ], [ 0, 1, 0 ], [ 0, 0, 1 ] ];
SystemEx.Math3D._corners = [ new Array(3), new Array(3) ];
SystemEx.Math3D.vertexNormals = [ [ -0.525731, 0, 0.850651 ], [ -0.442863, 0.238856, 0.864188 ], [ -0.295242, 0, 0.955423 ], [ -0.309017, 0.5, 0.809017 ], [ -0.16246, 0.262866, 0.951056 ], [ 0, 0, 1 ], [ 0, 0.850651, 0.525731 ], [ -0.147621, 0.716567, 0.681718 ], [ 0.147621, 0.716567, 0.681718 ], [ 0, 0.525731, 0.850651 ], [ 0.309017, 0.5, 0.809017 ], [ 0.525731, 0, 0.850651 ], [ 0.295242, 0, 0.955423 ], [ 0.442863, 0.238856, 0.864188 ], [ 0.16246, 0.262866, 0.951056 ], [ -0.681718, 0.147621, 0.716567 ], [ -0.809017, 0.309017, 0.5 ], [ -0.587785, 0.425325, 0.688191 ], [ -0.850651, 0.525731, 0 ], [ -0.864188, 0.442863, 0.238856 ], [ -0.716567, 0.681718, 0.147621 ], [ -0.688191, 0.587785, 0.425325 ], [ -0.5, 0.809017, 0.309017 ], [ -0.238856, 0.864188, 0.442863 ], [ -0.425325, 0.688191, 0.587785 ], [ -0.716567, 0.681718, -0.147621 ], [ -0.5, 0.809017, -0.309017 ], [ -0.525731, 0.850651, 0 ], [ 0, 0.850651, -0.525731 ], [ -0.238856, 0.864188, -0.442863 ], [ 0, 0.955423, -0.295242 ], [ -0.262866, 0.951056, -0.16246 ], [ 0, 1, 0 ], [ 0, 0.955423, 0.295242 ], [ -0.262866, 0.951056, 0.16246 ], [ 0.238856, 0.864188, 0.442863 ], [ 0.262866, 0.951056, 0.16246 ], [ 0.5, 0.809017, 0.309017 ], [ 0.238856, 0.864188, -0.442863 ], [ 0.262866, 0.951056, -0.16246 ], [ 0.5, 0.809017, -0.309017 ], [ 0.850651, 0.525731, 0 ], [ 0.716567, 0.681718, 0.147621 ], [ 0.716567, 0.681718, -0.147621 ], [ 0.525731, 0.850651, 0 ], [ 0.425325, 0.688191, 0.587785 ], [ 0.864188, 0.442863, 0.238856 ], [ 0.688191, 0.587785, 0.425325 ], [ 0.809017, 0.309017, 0.5 ], [ 0.681718, 0.147621, 0.716567 ], [ 0.587785, 0.425325, 0.688191 ], [ 0.955423, 0.295242, 0 ], [ 1, 0, 0 ], [ 0.951056, 0.16246, 0.262866 ], [ 0.850651, -0.525731, 0 ], [ 0.955423, -0.295242, 0 ], [ 0.864188, -0.442863, 0.238856 ], [ 0.951056, -0.16246, 0.262866 ], [ 0.809017, -0.309017, 0.5 ], [ 0.681718, -0.147621, 0.716567 ], [ 0.850651, 0, 0.525731 ], [ 0.864188, 0.442863, -0.238856 ], [ 0.809017, 0.309017, -0.5 ], [ 0.951056, 0.16246, -0.262866 ], [ 0.525731, 0, -0.850651 ], [ 0.681718, 0.147621, -0.716567 ], [ 0.681718, -0.147621, -0.716567 ], [ 0.850651, 0, -0.525731 ], [ 0.809017, -0.309017, -0.5 ], [ 0.864188, -0.442863, -0.238856 ], [ 0.951056, -0.16246, -0.262866 ], [ 0.147621, 0.716567, -0.681718 ], [ 0.309017, 0.5, -0.809017 ], [ 0.425325, 0.688191, -0.587785 ], [ 0.442863, 0.238856, -0.864188 ], [ 0.587785, 0.425325, -0.688191 ], [ 0.688191, 0.587785, -0.425325 ], [ -0.147621, 0.716567, -0.681718 ], [ -0.309017, 0.5, -0.809017 ], [ 0, 0.525731, -0.850651 ], [ -0.525731, 0, -0.850651 ], [ -0.442863, 0.238856, -0.864188 ], [ -0.295242, 0, -0.955423 ], [ -0.16246, 0.262866, -0.951056 ], [ 0, 0, -1 ], [ 0.295242, 0, -0.955423 ], [ 0.16246, 0.262866, -0.951056 ], [ -0.442863, -0.238856, -0.864188 ], [ -0.309017, -0.5, -0.809017 ], [ -0.16246, -0.262866, -0.951056 ], [ 0, -0.850651, -0.525731 ], [ -0.147621, -0.716567, -0.681718 ], [ 0.147621, -0.716567, -0.681718 ], [ 0, -0.525731, -0.850651 ], [ 0.309017, -0.5, -0.809017 ], [ 0.442863, -0.238856, -0.864188 ], [ 0.16246, -0.262866, -0.951056 ], [ 0.238856, -0.864188, -0.442863 ], [ 0.5, -0.809017, -0.309017 ], [ 0.425325, -0.688191, -0.587785 ], [ 0.716567, -0.681718, -0.147621 ], [ 0.688191, -0.587785, -0.425325 ], [ 0.587785, -0.425325, -0.688191 ], [ 0, -0.955423, -0.295242 ], [ 0, -1, 0 ], [ 0.262866, -0.951056, -0.16246 ], [ 0, -0.850651, 0.525731 ], [ 0, -0.955423, 0.295242 ], [ 0.238856, -0.864188, 0.442863 ], [ 0.262866, -0.951056, 0.16246 ], [ 0.5, -0.809017, 0.309017 ], [ 0.716567, -0.681718, 0.147621 ], [ 0.525731, -0.850651, 0 ], [ -0.238856, -0.864188, -0.442863 ], [ -0.5, -0.809017, -0.309017 ], [ -0.262866, -0.951056, -0.16246 ], [ -0.850651, -0.525731, 0 ], [ -0.716567, -0.681718, -0.147621 ], [ -0.716567, -0.681718, 0.147621 ], [ -0.525731, -0.850651, 0 ], [ -0.5, -0.809017, 0.309017 ], [ -0.238856, -0.864188, 0.442863 ], [ -0.262866, -0.951056, 0.16246 ], [ -0.864188, -0.442863, 0.238856 ], [ -0.809017, -0.309017, 0.5 ], [ -0.688191, -0.587785, 0.425325 ], [ -0.681718, -0.147621, 0.716567 ], [ -0.442863, -0.238856, 0.864188 ], [ -0.587785, -0.425325, 0.688191 ], [ -0.309017, -0.5, 0.809017 ], [ -0.147621, -0.716567, 0.681718 ], [ -0.425325, -0.688191, 0.587785 ], [ -0.16246, -0.262866, 0.951056 ], [ 0.442863, -0.238856, 0.864188 ], [ 0.16246, -0.262866, 0.951056 ], [ 0.309017, -0.5, 0.809017 ], [ 0.147621, -0.716567, 0.681718 ], [ 0, -0.525731, 0.850651 ], [ 0.425325, -0.688191, 0.587785 ], [ 0.587785, -0.425325, 0.688191 ], [ 0.688191, -0.587785, 0.425325 ], [ -0.955423, 0.295242, 0 ], [ -0.951056, 0.16246, 0.262866 ], [ -1, 0, 0 ], [ -0.850651, 0, 0.525731 ], [ -0.955423, -0.295242, 0 ], [ -0.951056, -0.16246, 0.262866 ], [ -0.864188, 0.442863, -0.238856 ], [ -0.951056, 0.16246, -0.262866 ], [ -0.809017, 0.309017, -0.5 ], [ -0.864188, -0.442863, -0.238856 ], [ -0.951056, -0.16246, -0.262866 ], [ -0.809017, -0.309017, -0.5 ], [ -0.681718, 0.147621, -0.716567 ], [ -0.681718, -0.147621, -0.716567 ], [ -0.850651, 0, -0.525731 ], [ -0.688191, 0.587785, -0.425325 ], [ -0.587785, 0.425325, -0.688191 ], [ -0.425325, 0.688191, -0.587785 ], [ -0.425325, -0.688191, -0.587785 ], [ -0.587785, -0.425325, -0.688191 ], [ -0.688191, -0.587785, -0.425325 ] ];
SystemEx.ByteBuilder._stringBuffer = new Array(2048);
SystemEx.ByteBuilder._errorHandler = null;
SystemEx.JSConvert.short_MinValue = -32768;
SystemEx.JSConvert.int_MinValue = -2147483648;
SystemEx.JSConvert.long_MinValue = -9223372036854775808;
SystemEx.JSConvert._wba = new Int8Array(4);
SystemEx.JSConvert._wia = new Int32Array(SystemEx.JSConvert._wba.buffer, 0, 1);
SystemEx.JSConvert._wfa = new Float32Array(SystemEx.JSConvert._wba.buffer, 0, 1);
(function () {
    ss.StringBuilder.prototype.baseAppend = ss.StringBuilder.prototype.append;
    ss.StringBuilder.prototype.baseClear = ss.StringBuilder.prototype.clear;
    ss.StringBuilder.prototype.append = function(s) { if (!ss.isNullOrUndefined(s)) this.length += s.length; return this.baseAppend(s); }
    ss.StringBuilder.prototype.clear = function(s) { this.length = 0; return this.baseClear(); }
    ss.StringBuilder.prototype.length = 0;
})();
SystemEx.Html.WebSocket.CONNECTING = 0;
SystemEx.Html.WebSocket.OPEN = 1;
SystemEx.Html.WebSocket.CLOSING = 2;
SystemEx.Html.WebSocket.CLOSED = 3;
SystemEx.Interop.CSyntax._conversionSpecification._defaultDigits = 6;
SystemEx.IO.FileInfo.separatorChar = '/';
SystemEx.IO.FileInfo.separator = '/';
SystemEx.IO.FileInfo.root = new SystemEx.IO.FileInfo('');
SystemEx.Security.Cryptography.CrcSlim._crC_INIT_VALUE = 65535;
SystemEx.Security.Cryptography.CrcSlim._crctable = [ 0, 4129, 8258, 12387, 16516, 20645, 24774, 28903, 33032, 37161, 41290, 45419, 49548, 53677, 57806, 61935, 4657, 528, 12915, 8786, 21173, 17044, 29431, 25302, 37689, 33560, 45947, 41818, 54205, 50076, 62463, 58334, 9314, 13379, 1056, 5121, 25830, 29895, 17572, 21637, 42346, 46411, 34088, 38153, 58862, 62927, 50604, 54669, 13907, 9842, 5649, 1584, 30423, 26358, 22165, 18100, 46939, 42874, 38681, 34616, 63455, 59390, 55197, 51132, 18628, 22757, 26758, 30887, 2112, 6241, 10242, 14371, 51660, 55789, 59790, 63919, 35144, 39273, 43274, 47403, 23285, 19156, 31415, 27286, 6769, 2640, 14899, 10770, 56317, 52188, 64447, 60318, 39801, 35672, 47931, 43802, 27814, 31879, 19684, 23749, 11298, 15363, 3168, 7233, 60846, 64911, 52716, 56781, 44330, 48395, 36200, 40265, 32407, 28342, 24277, 20212, 15891, 11826, 7761, 3696, 65439, 61374, 57309, 53244, 48923, 44858, 40793, 36728, 37256, 33193, 45514, 41451, 53516, 49453, 61774, 57711, 4224, 161, 12482, 8419, 20484, 16421, 28742, 24679, 33721, 37784, 41979, 46042, 49981, 54044, 58239, 62302, 689, 4752, 8947, 13010, 16949, 21012, 25207, 29270, 46570, 42443, 38312, 34185, 62830, 58703, 54572, 50445, 13538, 9411, 5280, 1153, 29798, 25671, 21540, 17413, 42971, 47098, 34713, 38840, 59231, 63358, 50973, 55100, 9939, 14066, 1681, 5808, 26199, 30326, 17941, 22068, 55628, 51565, 63758, 59695, 39368, 35305, 47498, 43435, 22596, 18533, 30726, 26663, 6336, 2273, 14466, 10403, 52093, 56156, 60223, 64286, 35833, 39896, 43963, 48026, 19061, 23124, 27191, 31254, 2801, 6864, 10931, 14994, 64814, 60687, 56684, 52557, 48554, 44427, 40424, 36297, 31782, 27655, 23652, 19525, 15522, 11395, 7392, 3265, 61215, 65342, 53085, 57212, 44955, 49082, 36825, 40952, 28183, 32310, 20053, 24180, 11923, 16050, 3793, 7920 ];
SystemEx.Security.Cryptography.CrcSlim._chktbl = [ 132, 71, 81, 193, 147, 34, 33, 36, 47, 102, 96, 77, 176, 124, 218, 136, 84, 21, 43, 198, 108, 137, 197, 157, 72, 238, 230, 138, 181, 244, 203, 251, 241, 12, 46, 160, 215, 201, 31, 214, 6, 154, 9, 65, 84, 103, 70, 199, 116, 227, 200, 182, 93, 166, 54, 196, 171, 44, 126, 133, 168, 164, 166, 77, 150, 25, 25, 154, 204, 216, 172, 57, 94, 60, 242, 245, 90, 114, 229, 169, 209, 179, 35, 130, 111, 41, 203, 209, 204, 113, 251, 234, 146, 235, 28, 202, 76, 112, 254, 77, 201, 103, 67, 71, 148, 185, 71, 188, 63, 1, 171, 123, 166, 226, 118, 239, 90, 122, 41, 11, 81, 84, 103, 216, 28, 20, 62, 41, 236, 233, 45, 72, 103, 255, 237, 84, 79, 72, 192, 170, 97, 247, 120, 18, 3, 122, 158, 139, 207, 131, 123, 174, 202, 123, 217, 233, 83, 42, 235, 210, 216, 205, 163, 16, 37, 120, 90, 181, 35, 6, 147, 183, 132, 210, 189, 150, 117, 165, 94, 207, 78, 233, 80, 161, 230, 157, 177, 227, 133, 102, 40, 78, 67, 220, 110, 187, 51, 158, 243, 13, 0, 193, 207, 103, 52, 6, 124, 113, 227, 99, 183, 183, 223, 146, 196, 194, 37, 92, 255, 195, 110, 252, 170, 30, 42, 72, 17, 28, 54, 104, 120, 134, 121, 48, 195, 214, 222, 188, 58, 42, 109, 30, 70, 221, 224, 128, 30, 68, 59, 111, 175, 49, 218, 162, 189, 119, 6, 86, 192, 183, 146, 75, 55, 192, 252, 194, 213, 251, 168, 218, 245, 87, 168, 24, 192, 223, 231, 170, 42, 224, 124, 111, 119, 177, 38, 186, 249, 46, 29, 22, 203, 184, 162, 68, 213, 47, 26, 121, 116, 135, 75, 0, 201, 74, 58, 101, 143, 230, 93, 229, 10, 119, 216, 26, 20, 65, 117, 177, 226, 80, 44, 147, 56, 43, 109, 243, 246, 219, 31, 205, 255, 20, 112, 231, 22, 232, 61, 240, 227, 188, 94, 182, 63, 204, 129, 36, 103, 243, 151, 59, 254, 58, 150, 133, 223, 228, 110, 60, 133, 5, 14, 163, 43, 7, 200, 191, 229, 19, 130, 98, 8, 97, 105, 75, 71, 98, 115, 68, 100, 142, 226, 145, 166, 154, 183, 233, 4, 182, 84, 12, 197, 169, 71, 166, 201, 8, 254, 78, 166, 204, 138, 91, 144, 111, 43, 63, 182, 10, 150, 192, 120, 88, 60, 118, 109, 148, 26, 228, 78, 184, 56, 187, 245, 235, 41, 216, 176, 243, 21, 30, 153, 150, 60, 93, 99, 213, 177, 173, 82, 184, 85, 112, 117, 62, 26, 213, 218, 246, 122, 72, 125, 68, 65, 249, 17, 206, 215, 202, 165, 61, 122, 121, 126, 125, 37, 27, 119, 188, 247, 199, 15, 132, 149, 16, 146, 103, 21, 17, 90, 94, 65, 102, 15, 56, 3, 178, 241, 93, 248, 171, 192, 2, 118, 132, 40, 244, 157, 86, 70, 96, 32, 219, 104, 167, 187, 238, 172, 21, 1, 47, 32, 9, 219, 192, 22, 161, 137, 249, 148, 89, 0, 193, 118, 191, 193, 77, 93, 45, 169, 133, 44, 214, 211, 20, 204, 2, 195, 194, 250, 107, 183, 166, 239, 221, 18, 38, 164, 99, 227, 98, 189, 86, 138, 82, 43, 185, 223, 9, 188, 14, 151, 169, 176, 130, 70, 8, 213, 26, 142, 27, 167, 144, 152, 185, 187, 60, 23, 154, 242, 130, 186, 100, 10, 127, 202, 90, 140, 124, 211, 121, 9, 91, 38, 187, 189, 37, 223, 61, 111, 154, 143, 238, 33, 102, 176, 141, 132, 76, 145, 69, 212, 119, 79, 179, 140, 188, 168, 153, 170, 25, 83, 124, 2, 135, 187, 11, 124, 26, 45, 223, 72, 68, 6, 214, 125, 12, 45, 53, 118, 174, 196, 95, 113, 133, 151, 196, 61, 239, 82, 190, 0, 228, 205, 73, 209, 209, 28, 60, 208, 28, 66, 175, 212, 189, 88, 52, 7, 50, 238, 185, 181, 234, 255, 215, 140, 13, 46, 47, 175, 135, 187, 230, 82, 113, 34, 245, 37, 23, 161, 130, 4, 194, 74, 189, 87, 198, 171, 200, 53, 12, 60, 217, 194, 67, 219, 39, 146, 207, 184, 37, 96, 250, 33, 59, 4, 82, 200, 150, 186, 116, 227, 103, 62, 142, 141, 97, 144, 146, 89, 182, 26, 28, 94, 33, 193, 101, 229, 166, 52, 5, 111, 197, 96, 177, 131, 193, 213, 213, 237, 217, 199, 17, 123, 73, 122, 249, 249, 132, 71, 155, 226, 165, 130, 224, 194, 136, 208, 178, 88, 136, 127, 69, 9, 103, 116, 97, 191, 230, 64, 226, 157, 194, 71, 5, 137, 237, 203, 187, 183, 39, 231, 220, 122, 253, 191, 168, 208, 170, 16, 57, 60, 32, 240, 211, 110, 177, 114, 248, 230, 15, 239, 55, 229, 9, 51, 90, 131, 67, 128, 79, 101, 47, 124, 140, 106, 160, 130, 12, 212, 212, 250, 129, 96, 61, 223, 6, 241, 95, 8, 13, 109, 67, 242, 227, 17, 125, 128, 50, 197, 251, 197, 217, 39, 236, 198, 78, 101, 39, 118, 135, 166, 238, 238, 215, 139, 209, 160, 92, 176, 66, 19, 14, 149, 74, 242, 6, 198, 67, 51, 244, 199, 248, 231, 31, 221, 228, 70, 74, 112, 57, 108, 208, 237, 202, 190, 96, 59, 209, 123, 87, 72, 229, 58, 121, 193, 105, 51, 83, 27, 128, 184, 145, 125, 180, 246, 23, 26, 29, 90, 50, 214, 204, 113, 41, 63, 40, 187, 243, 94, 113, 184, 67, 175, 248, 185, 100, 239, 196, 165, 108, 8, 83, 199, 0, 16, 57, 79, 221, 228, 182, 25, 39, 251, 184, 245, 50, 115, 229, 203, 50, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ];
SystemEx.Security.Cryptography.CrcSlim._chkb = new Array(60 + 4);
SystemEx.Security.Cryptography.Md4Slim._blocK_LENGTH = 64;
SystemEx.Text.PackText.maX_INFO_KEY = 64;
SystemEx.Text.PackText.maX_INFO_VALUE = 64;
SystemEx.Text.PackText.maX_INFO_STRING = 512;
SystemEx.Text.PackText.SPACES = '                     ';
SystemEx.Text.PackText._errorHandler = null;

}
ss.loader.registerScript('Script.WebEx', [], executeScript);
})();

//! This script was generated using Script# v0.6.1.0
