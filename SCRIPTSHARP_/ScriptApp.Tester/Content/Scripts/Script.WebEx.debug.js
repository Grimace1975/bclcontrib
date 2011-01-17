//! Script.WebEx.debug.js
//

(function() {
function executeScript() {

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


Type.registerNamespace('SystemEx');

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
    /// <param name="source" type="Object">
    /// </param>
    /// <param name="sourceIndex" type="Number" integer="true">
    /// </param>
    /// <param name="destination" type="Object">
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
// SystemEx.JSConvertEx

SystemEx.JSConvertEx = function SystemEx_JSConvertEx() {
    /// <field name="_wba" type="Int8Array" static="true">
    /// </field>
    /// <field name="_wia" type="Int32Array" static="true">
    /// </field>
    /// <field name="_wfa" type="Float32Array" static="true">
    /// </field>
}
SystemEx.JSConvertEx.singleToIntBits = function SystemEx_JSConvertEx$singleToIntBits(v) {
    /// <param name="v" type="Number">
    /// </param>
    /// <returns type="Number" integer="true"></returns>
    SystemEx.JSConvertEx._wfa[0] = v;
    return SystemEx.JSConvertEx._wia[0];
}
SystemEx.JSConvertEx.intBitsToSingle = function SystemEx_JSConvertEx$intBitsToSingle(v) {
    /// <param name="v" type="Number" integer="true">
    /// </param>
    /// <returns type="Number"></returns>
    SystemEx.JSConvertEx._wia[0] = v;
    return SystemEx.JSConvertEx._wfa[0];
}
SystemEx.JSConvertEx.bytesToJSArray = function SystemEx_JSConvertEx$bytesToJSArray(data) {
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
SystemEx.JSConvertEx.uBytesToJSArray = function SystemEx_JSConvertEx$uBytesToJSArray(data) {
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
SystemEx.JSConvertEx.singlesToJSArray = function SystemEx_JSConvertEx$singlesToJSArray(data) {
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
SystemEx.JSConvertEx.doublesToJSArray = function SystemEx_JSConvertEx$doublesToJSArray(data) {
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
SystemEx.JSConvertEx.ints16ToJSArray = function SystemEx_JSConvertEx$ints16ToJSArray(data) {
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
SystemEx.JSConvertEx.uInt16ToJSArray = function SystemEx_JSConvertEx$uInt16ToJSArray(data) {
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
SystemEx.JSConvertEx.ints32ToJSArray = function SystemEx_JSConvertEx$ints32ToJSArray(data) {
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
    
    get_buffer: function SystemEx_IO_MemoryStream$get_buffer() {
        /// <value type="Array" elementType="Number" elementInteger="true"></value>
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
    return SystemEx.JSConvertEx.intBitsToSingle(SystemEx.IO.SE.readInt32(s));
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
    s.writeByte((v) ? 1 : 0);
}
SystemEx.IO.SE.writeChar = function SystemEx_IO_SE$writeChar(s, v) {
    /// <param name="s" type="SystemEx.IO.Stream">
    /// </param>
    /// <param name="v" type="Number" integer="true">
    /// </param>
    s.writeByte(v >> 8);
    s.writeByte(v);
}
SystemEx.IO.SE.writeInt16 = function SystemEx_IO_SE$writeInt16(s, v) {
    /// <param name="s" type="SystemEx.IO.Stream">
    /// </param>
    /// <param name="v" type="Number" integer="true">
    /// </param>
    s.writeByte(v >> 8);
    s.writeByte(v);
}
SystemEx.IO.SE.writeInt32 = function SystemEx_IO_SE$writeInt32(s, v) {
    /// <param name="s" type="SystemEx.IO.Stream">
    /// </param>
    /// <param name="v" type="Number" integer="true">
    /// </param>
    s.writeByte(v >> 24);
    s.writeByte(v >> 16);
    s.writeByte(v >> 8);
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
    SystemEx.IO.SE.writeInt32(s, SystemEx.JSConvertEx.singleToIntBits(v));
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
            baos.writeByte(192 | (31 & (c >> 6)));
            baos.writeByte(128 | (63 & c));
        }
        else {
            baos.writeByte(224 | (15 & (c >> 12)));
            baos.writeByte(128 | (63 & (c >> 6)));
            baos.writeByte(128 | (63 & c));
        }
    }
    SystemEx.IO.SE.writeInt16(s, baos.get_length());
    s.write(baos.get_buffer(), 0, baos.get_length());
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
            var raf = new SystemEx.IO.FileStream(this, SystemEx.IO.FileMode.append, SystemEx.IO.FileAccess.read);
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

SystemEx.IO.FileStream = function SystemEx_IO_FileStream(fileInfo, fileMode, fileAccess) {
    /// <param name="fileInfo" type="SystemEx.IO.FileInfo">
    /// </param>
    /// <param name="fileMode" type="SystemEx.IO.FileMode">
    /// </param>
    /// <param name="fileAccess" type="SystemEx.IO.FileAccess">
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
            this._flush$1();
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
            this._flush$1();
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
    
    _flush$1: function SystemEx_IO_FileStream$_flush$1() {
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
    }
}


SystemEx.Html.CloseEventArgs.registerClass('SystemEx.Html.CloseEventArgs');
SystemEx.Html.MessageEventArgs.registerClass('SystemEx.Html.MessageEventArgs');
SystemEx.Html.WebSocket.registerClass('SystemEx.Html.WebSocket');
SystemEx.Html.LocalStorage.registerClass('SystemEx.Html.LocalStorage');
SystemEx.JSArrayEx.registerClass('SystemEx.JSArrayEx');
SystemEx.JSConvertEx.registerClass('SystemEx.JSConvertEx');
SystemEx.StringBuilderEx.registerClass('SystemEx.StringBuilderEx');
SystemEx.IO.Stream.registerClass('SystemEx.IO.Stream');
SystemEx.IO.MemoryStream.registerClass('SystemEx.IO.MemoryStream', SystemEx.IO.Stream);
SystemEx.IO.SE.registerClass('SystemEx.IO.SE');
SystemEx.IO.FileInfo.registerClass('SystemEx.IO.FileInfo');
SystemEx.IO.FileStream.registerClass('SystemEx.IO.FileStream', SystemEx.IO.Stream);
SystemEx.IO.Path.registerClass('SystemEx.IO.Path');
SystemEx.IO.Directory.registerClass('SystemEx.IO.Directory');
SystemEx.IO.File.registerClass('SystemEx.IO.File');
SystemEx.Html.WebSocket.CONNECTING = 0;
SystemEx.Html.WebSocket.OPEN = 1;
SystemEx.Html.WebSocket.CLOSING = 2;
SystemEx.Html.WebSocket.CLOSED = 3;
SystemEx.JSConvertEx._wba = new Int8Array(4);
SystemEx.JSConvertEx._wia = new Int32Array(SystemEx.JSConvertEx._wba.buffer, 0, 1);
SystemEx.JSConvertEx._wfa = new Float32Array(SystemEx.JSConvertEx._wba.buffer, 0, 1);
(function () {
    ss.StringBuilder.prototype.baseAppend = ss.StringBuilder.prototype.append;
    ss.StringBuilder.prototype.baseClear = ss.StringBuilder.prototype.clear;
    ss.StringBuilder.prototype.append = function(s) { if (!ss.isNullOrUndefined(s)) this.length += s.length; return this.baseAppend(s); }
    ss.StringBuilder.prototype.clear = function(s) { this.length = 0; return this.baseClear(); }
    ss.StringBuilder.prototype.length = 0;
})();
SystemEx.IO.FileInfo.separatorChar = '/';
SystemEx.IO.FileInfo.separator = '/';
SystemEx.IO.FileInfo.root = new SystemEx.IO.FileInfo('');

}
ss.loader.registerScript('Script.WebEx', [], executeScript);
})();

//! This script was generated using Script# v0.6.1.0
