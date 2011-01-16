// Script.WebEx.js
(function(){function executeScript(){
Type.registerNamespace('SystemEx');SystemEx.JSArrayEx=function(){}
SystemEx.JSArrayEx.clear=function(array,index,length){}
SystemEx.JSArrayEx.copy=function(sourceArray,sourceIndex,destinationArray,destinationIndex,index){System.Arr;}
SystemEx.JSConvertEx=function(){}
SystemEx.JSConvertEx.singleToIntBits=function(v){SystemEx.JSConvertEx.$2.set3(0,v);return SystemEx.JSConvertEx.$1.get(0);}
SystemEx.JSConvertEx.intBitsToSingle=function(v){SystemEx.JSConvertEx.$1.set3(0,v);return SystemEx.JSConvertEx.$2.get(0);}
SystemEx.JSConvertEx.bytesToJSArray=function(data){var $0=[];var $1=data.length;for(var $2=$1-1;$2>=0;$2--){$0.set($2,data[$2]);}return $0;}
SystemEx.JSConvertEx.uBytesToJSArray=function(data){var $0=[];var $1=data.length;for(var $2=$1-1;$2>=0;$2--){$0.set($2,data[$2]&255);}return $0;}
SystemEx.JSConvertEx.singlesToJSArray=function(data){var $0=[];var $1=data.length;for(var $2=$1-1;$2>=0;$2--){$0.set($2,data[$2]);}return $0;}
SystemEx.JSConvertEx.doublesToJSArray=function(data){var $0=[];var $1=data.length;for(var $2=$1-1;$2>=0;$2--){$0.set($2,data[$2]);}return $0;}
SystemEx.JSConvertEx.ints16ToJSArray=function(data){var $0=[];var $1=data.length;for(var $2=$1-1;$2>=0;$2--){$0.set($2,data[$2]);}return $0;}
SystemEx.JSConvertEx.uInt16ToJSArray=function(data){var $0=[];var $1=data.length;for(var $2=$1-1;$2>=0;$2--){$0.set($2,data[$2]&65535);}return $0;}
SystemEx.JSConvertEx.ints32ToJSArray=function(data){var $0=[];var $1=data.length;for(var $2=$1-1;$2>=0;$2--){$0.set($2,data[$2]);}return $0;}
SystemEx.JSArrayString=function(){}
SystemEx.JSArrayString.prototype={get:function(index){return this[index];},joinA:function(){return this.join(',');},join:function(separator){return this.join(separator);},get_length:function(){return this.length;},set_length:function(value){this.length = value;return value;},push:function(value){this[this.length] = value;},set:function(index,value){this[index] = value;},shift:function(){return this.shift();},unshift:function(value){this.unshift(value);;}}
SystemEx.JSArrayNumber=function(){}
SystemEx.JSArrayNumber.prototype={get:function(index){return this[index];},joinA:function(){return this.join(',');},join:function(separator){return this.join(separator);},get_length:function(){return this.length;},set_length:function(value){this.length = value;return value;},push:function(value){this[this.length] = value;},set:function(index,value){this[index] = value;},shift:function(){return this.shift();},unshift:function(value){this.unshift(value);;}}
SystemEx.JSArrayMixed=function(){}
SystemEx.JSArrayMixed.prototype={get:function(index){return this[index];},joinA:function(){return this.join(',');},join:function(separator){return this.join(separator);},get_length:function(){return this.length;},set_length:function(value){this.length = value;return value;},push:function(value){this[this.length] = value;},set:function(index,value){this[index] = value;},shift:function(){return this.shift();},unshift:function(value){this.unshift(value);;}}
SystemEx.JSArrayInteger=function(){}
SystemEx.JSArrayInteger.prototype={get:function(index){return this[index];},joinA:function(){return this.join(',');},join:function(separator){return this.join(separator);},get_length:function(){return this.length;},set_length:function(value){this.length = value;return value;},push:function(value){this[this.length] = value;},set:function(index,value){this[index] = value;},shift:function(){return this.shift();},unshift:function(value){this.unshift(value);;}}
SystemEx.JSArrayBoolean=function(){}
SystemEx.JSArrayBoolean.prototype={get:function(index){return this[index];},joinA:function(){return this.join(',');},join:function(separator){return this.join(separator);},get_length:function(){return this.length;},set_length:function(value){this.length = value;return value;},push:function(value){this[this.length] = value;},set:function(index,value){this[index] = value;},shift:function(){return this.shift();},unshift:function(value){this.unshift(value);;}}
SystemEx.JSArray=function(){}
SystemEx.JSArray.prototype={get:function(index){return this[index];},joinA:function(){return this.join(',');},join:function(separator){return this.join(separator);},get_length:function(){return this.length;},set_length:function(value){this.length = value;return value;},push:function(value){this[this.length] = value;},set:function(index,value){this[index] = value;},shift:function(){return this.shift();},unshift:function(value){this.unshift(value);;}}
SystemEx.StringBuilderEx=function(){}
SystemEx.StringBuilderEx.getLength=function(b){return ;}
Type.registerNamespace('SystemEx.IO');SystemEx.IO.FileAccess=function(){};SystemEx.IO.FileAccess.prototype = {read:1,readWrite:3,write:2}
SystemEx.IO.FileAccess.registerEnum('SystemEx.IO.FileAccess',false);SystemEx.IO.FileMode=function(){};SystemEx.IO.FileMode.prototype = {append:6,create:2,createNew:1,open:3,openOrCreate:4,truncate:5}
SystemEx.IO.FileMode.registerEnum('SystemEx.IO.FileMode',false);SystemEx.IO.MemoryStream=function(buffer){SystemEx.IO.MemoryStream.initializeBase(this);this.$1=((buffer==null)?SystemEx.IO.MemoryStream.makeBuffer(16):buffer);}
SystemEx.IO.MemoryStream.makeBuffer=function(initialSize){return new Array((initialSize!==0)?initialSize:16);}
SystemEx.IO.MemoryStream.prototype={$0:0,$1:null,$2:0,get_buffer:function(){return this.$1;},toArray:function(){var $0=new Array(this.$0);SystemEx.JSArrayEx.copy(this.$1,0,$0,0,this.$0);return $0;},get_length:function(){return this.$0;},get_position:function(){return this.$2;},readByte:function(){return ((this.$2<this.$1.length)?this.$1[this.$2++]:-1);},writeByte:function(b){if(this.$1.length===this.$0){var $0=new Array(this.$1.length*3/2);SystemEx.JSArrayEx.copy(this.$1,0,$0,0,this.$0);this.$1=$0;}this.$1[this.$0++]=b;},close:function(){this.$1=null;}}
SystemEx.IO.SE=function(){}
SystemEx.IO.SE.internalReadByte=function(s){var $0=s.readByte();if($0===-1){throw new Error('IOException: EOF');}return $0;}
SystemEx.IO.SE.$0=function($p0){var $0=$p0.readByte();var $1=SystemEx.IO.SE.internalReadByte($p0);return (($0<<8)|$1);}
SystemEx.IO.SE.readByte=function(s){var $0=s.readByte();if($0===-1){throw new Error('IOException: EOF');}return $0;}
SystemEx.IO.SE.readBytes=function(s,b,offset,length){if((offset===0)&&(length===0)){length=b.length;}while(length>0){var $0=s.read(b,offset,length);if($0<=0){throw new Error('IOException: EOF');}offset+=$0;length-=$0;}}
SystemEx.IO.SE.readBoolean=function(s){return (SystemEx.IO.SE.readByte(s)!==0);}
SystemEx.IO.SE.readChar=function(s){var $0=s.readByte();var $1=SystemEx.IO.SE.internalReadByte(s);return (($0<<8)|$1);}
SystemEx.IO.SE.readInt16=function(s){var $0=s.readByte();var $1=SystemEx.IO.SE.internalReadByte(s);return (($0<<8)|$1);}
SystemEx.IO.SE.readInt32=function(s){var $0=s.readByte();var $1=s.readByte();var $2=s.readByte();var $3=SystemEx.IO.SE.internalReadByte(s);return ($0<<24)|($1<<16)|($2<<8)|$3;}
SystemEx.IO.SE.readInt64=function(s){var $0=SystemEx.IO.SE.readInt32(s);var $1=SystemEx.IO.SE.readInt32(s)&4294967295;return ($0<<32)|$1;}
SystemEx.IO.SE.readSingle=function(s){return SystemEx.JSConvertEx.intBitsToSingle(SystemEx.IO.SE.readInt32(s));}
SystemEx.IO.SE.readDouble=function(s){throw new Error('NotSupportedException: readDouble');}
SystemEx.IO.SE.readString=function(s){var $0=SystemEx.IO.SE.$0(s);var $1=new ss.StringBuilder();while($0>0){$0-=SystemEx.IO.SE.$1(s,$1);}return $1.toString();}
SystemEx.IO.SE.readStringLine=function(s){throw new Error('NotSupportedException: ReadLine');}
SystemEx.IO.SE.$1=function($p0,$p1){var $0=SystemEx.IO.SE.internalReadByte($p0);if(($0&128)===0){$p1.append($0);return 1;}if(($0&224)===176){var $1=SystemEx.IO.SE.internalReadByte($p0);$p1.append(((($0&31)<<6)|($1&63)));return 2;}if(($0&240)===224){var $2=$p0.readByte();var $3=SystemEx.IO.SE.internalReadByte($p0);$p1.append(((($0&15)<<12)|(($2&63)<<6)|($3&63)));return 3;}throw new Error('IOException: UTFDataFormat:');}
SystemEx.IO.SE.writeByte=function(s,v){s.writeByte(v);}
SystemEx.IO.SE.writeBytes=function(s,v){var $0=v.length;for(var $1=0;$1<$0;$1++){s.writeByte(v.charAt($1)&255);}}
SystemEx.IO.SE.writeBoolean=function(s,v){s.writeByte((v)?1:0);}
SystemEx.IO.SE.writeChar=function(s,v){s.writeByte(v>>8);s.writeByte(v);}
SystemEx.IO.SE.writeInt16=function(s,v){s.writeByte(v>>8);s.writeByte(v);}
SystemEx.IO.SE.writeInt32=function(s,v){s.writeByte(v>>24);s.writeByte(v>>16);s.writeByte(v>>8);s.writeByte(v);}
SystemEx.IO.SE.writeInt64=function(s,v){SystemEx.IO.SE.writeInt32(s,(v>>32));SystemEx.IO.SE.writeInt32(s,v);}
SystemEx.IO.SE.writeSingle=function(s,v){SystemEx.IO.SE.writeInt32(s,SystemEx.JSConvertEx.singleToIntBits(v));}
SystemEx.IO.SE.writeDouble=function(s,v){throw new Error('NotSupportedException: writeDouble');}
SystemEx.IO.SE.writeString=function(s,v){var $0=new SystemEx.IO.MemoryStream();for(var $1=0;$1<v.length;$1++){var $2=v.charAt($1);if(($2>0)&&($2<80)){$0.writeByte($2);}else if($2<'\u0800'){$0.writeByte(192|(31&($2>>6)));$0.writeByte(128|(63&$2));}else{$0.writeByte(224|(15&($2>>12)));$0.writeByte(128|(63&($2>>6)));$0.writeByte(128|(63&$2));}}SystemEx.IO.SE.writeInt16(s,$0.get_length());s.write($0.get_buffer(),0,$0.get_length());}
SystemEx.IO.FileInfo=function(fileName,parent){while(fileName.endsWith('//')&&(fileName.length>0)){fileName=fileName.substring(0,fileName.length-1);}var $0=fileName.lastIndexOf('/');if($0===-1){this.$1=fileName;}else if($0===0){this.$1=fileName.substring($0,fileName.length);this.$0=((this.$1==='')?null:SystemEx.IO.FileInfo.root);}else{this.$1=fileName.substring($0+1,fileName.length);this.$0=new SystemEx.IO.FileInfo(fileName.substring(0,$0));}}
SystemEx.IO.FileInfo.prototype={$0:null,$1:null,get_name:function(){return this.$1;},get_$2:function(){return ((this.$0==null)?'':this.$0.get_path());},get_$3:function(){return this.$0;},get_path:function(){return ((this.$0==null)?this.$1:this.$0.get_path()+'/'+this.$1);},$4:function(){return ((this.$1==='')&&(this.$0==null));},$5:function(){if(this.$4()){return true;}if(this.$0==null){return false;}return this.$0.$5();},$6:function(){var $0=this.$7().get_path();return (($0.length===0)?'/':$0);},$7:function(){if(this.$5()){return this;}return new SystemEx.IO.FileInfo(this.$1,(this.$0==null)?SystemEx.IO.FileInfo.root:this.$0.$7());},$8:function(){return this.$9().$6();},$9:function(){var $0=((this.$0==null)?null:this.$0.$9());if(this.$1==='.'){return (($0==null)?SystemEx.IO.FileInfo.root:$0);}if(($0!=null)&&($0.$1==='')){$0=null;}if(this.$1==='..'){if($0==null){return SystemEx.IO.FileInfo.root;}if($0.$0==null){return SystemEx.IO.FileInfo.root;}return $0.$0;}return new SystemEx.IO.FileInfo(this.$1,(($0==null)&&(this.$1!==''))?SystemEx.IO.FileInfo.root:$0);},$A:function(){try{return (SystemEx.Html.LocalStorage.getItem(this.$8())!=null);}catch($0){if($0.message.startsWith('IOException')){return false;}throw $0;}},$B:function(){try{var $0=SystemEx.Html.LocalStorage.getItem(this.$8());return (($0!=null)&&!$0.startsWith('{'));}catch($1){if($1.message.startsWith('IOException')){return false;}throw $1;}},get_length:function(){try{if(!this.$A()){return 0;}var $0=new SystemEx.IO.FileStream(this,6,1);var $1=$0.get_length();$0.close();return $1;}catch($2){if($2.message.startsWith('IOException')){return 0;}throw $2;}},$C:function(){if(this.$A()||!this.$0.$A()){return false;}SystemEx.Html.LocalStorage.setItem(this.$8(),SystemEx.Html.WindowEx.btoa(''));return true;},$D:function(){try{if(!this.$A()){return false;}SystemEx.Html.LocalStorage.removeItem(this.$8());return true;}catch($0){if($0.message.startsWith('IOException')){return false;}throw $0;}},$E:function(){try{if((this.$0!=null)&&!this.$0.$A()){return false;}if(this.$A()){return false;}SystemEx.Html.LocalStorage.setItem(this.$8(),'{}');return true;}catch($0){if($0.message.startsWith('IOException')){return false;}throw $0;}},$F:function(){if(this.$0!=null){this.$0.$F();}return this.$E();},$10:function($p0){var $0=[];try{var $1=this.$8();if(!$1.endsWith('//')){$1+='/';}var $2=$1.length;var $3=SystemEx.Html.LocalStorage.get_length();for(var $4=0;$4<$3;$4++){var $5=SystemEx.Html.LocalStorage.key($4);if($5.startsWith($1)&&($5.indexOf('/',$2)===-1)){var $6=$5.substring($2,$5.length);if(($p0==null)||$p0.invoke($6,this)){$0.add(new SystemEx.IO.FileInfo($6,this));}}}}catch($7){}return $0;}}
SystemEx.IO.FileStream=function(fileInfo,fileMode,fileAccess){SystemEx.IO.FileStream.initializeBase(this);this.$0=fileInfo.$8();if((fileAccess!==1)&&(fileAccess!==3)){throw new Error('IllegalArgumentException: fileAccess');}this.$1=(fileAccess===3);if(fileInfo.$A()){try{this.$3=SystemEx.Html.WindowEx.atob(SystemEx.Html.LocalStorage.getItem(this.$0));this.$7=this.$3.length;}catch($0){throw (($0.message.startsWith('IOException'))?new Error('FileNotFoundException:'+$0):$0);}}else if(this.$1){this.$3='';this.$2=true;try{this.$9();}catch($1){throw (($1.message.startsWith('IOException'))?new Error('FileNotFoundException:'+$1):$1);}}else{throw new Error('FileNotFoundException:'+this.$0);}}
SystemEx.IO.FileStream.prototype={$0:null,$1:false,$2:false,$3:null,$4:0,$5:null,$6:0,$7:0,get_filePointer:function(){return this.$6;},seek:function(position){if(position<0){throw new Error('IllegalArgumentException:');}this.$6=position;},get_length:function(){return this.$7;},set_length:function(value){if(this.$7!==value){this.$8();if(this.$3.length>value){this.$3=this.$3.substring(0,value);this.$7=value;}else{while(this.$7<value){this.writeByte(0);}}}return value;},get_poition:function(){return this.$6;},close:function(){if(this.$3!=null){this.$9();this.$3=null;}},$8:function(){if(this.$5==null){return;}if(this.$3.length<this.$4){var $1=new ss.StringBuilder();while(this.$3.length+SystemEx.StringBuilderEx.getLength($1)<this.$4){$1.append('\u0000');}this.$3+=$1.toString();}var $0=this.$4+SystemEx.StringBuilderEx.getLength(this.$5);this.$3=this.$3.substring(0,this.$4)+this.$5.toString()+(($0<this.$3.length)?this.$3.substring($0,this.$3.length):'');this.$5=null;},$9:function(){if(!this.$2){return;}this.$8();SystemEx.Html.LocalStorage.setItem(this.$0,SystemEx.Html.WindowEx.btoa(this.$3));this.$2=false;},readByte:function(){if(this.$6>=this.$7){return -1;}else{this.$8();return this.$3.charAt(this.$6++);}},writeByte:function(b){if(!this.$1){throw new Error('IOException: not writeable');}if(this.$5==null){this.$4=this.$6;this.$5=new ss.StringBuilder();}else if(this.$4+SystemEx.StringBuilderEx.getLength(this.$5)!==this.$6){this.$8();this.$4=this.$6;this.$5=new ss.StringBuilder();}this.$5.append((b&255));this.$6++;this.$7=Math.max(this.$6,this.$7);this.$2=true;}}
SystemEx.IO.Path=function(){}
SystemEx.IO.Directory=function(){}
SystemEx.IO.Directory.exists=function(path){if(path==null){throw new Error('ArgumentNullException: path');}return new SystemEx.IO.FileInfo(path).$A();}
SystemEx.IO.File=function(){}
SystemEx.IO.File.delete_=function(path){if(path==null){throw new Error('ArgumentNullException: path');}return new SystemEx.IO.FileInfo(path).$D();}
SystemEx.IO.File.exists=function(path){if(path==null){throw new Error('ArgumentNullException: path');}return new SystemEx.IO.FileInfo(path).$A();}
SystemEx.IO.Stream=function(){}
SystemEx.IO.Stream.prototype={read:function(b,offset,length){if((offset===0)&&(length===0)){length=b.length;}var $0=offset+length;for(var $1=offset;$1<$0;$1++){var $2=this.readByte();if($2===-1){return (($1===offset)?-1:$1-offset);}b[$1]=$2;}return length;},write:function(b,offset,length){if((offset===0)&&(length===0)){length=b.length;}var $0=offset+length;for(var $1=offset;$1<$0;$1++){this.writeByte(b[$1]);}}}
Type.registerNamespace('SystemEx.Html');SystemEx.Html.WindowEx=function(){}
SystemEx.Html.WindowEx.btoa=function(s){return window.btoa(s);}
SystemEx.Html.WindowEx.atob=function(s){return window.atob(s);}
SystemEx.Html.LocalStorage=function(){}
SystemEx.Html.LocalStorage.getItem=function(key){try{return window.localStorage.getItem(key);}catch($0){throw new Error('IOException:'+$0);}}
SystemEx.Html.LocalStorage.key=function(index){try{return window.localStorage.key(index);}catch($0){throw new Error('IOException:'+$0);}}
SystemEx.Html.LocalStorage.get_length=function(){try{return window.localStorage.length;}catch($0){throw new Error('IOException:'+$0);}}
SystemEx.Html.LocalStorage.removeItem=function(key){try{window.localStorage.removeItem(key);}catch($0){throw new Error('IOException:'+$0);}}
SystemEx.Html.LocalStorage.setItem=function(key,value){try{window.localStorage.setItem(key, value);}catch($0){throw new Error('IOException:'+$0);}}
Type.registerNamespace('SystemEx.TypedArrays');SystemEx.TypedArrays.ArrayBufferView=function(){}
SystemEx.TypedArrays.ArrayBufferView.prototype={get_buffer:function(){return this.buffer;},get_byteLength:function(){return this.byteLength;},get_byteOffset:function(){return this.byteOffset;}}
SystemEx.TypedArrays.ArrayBuffer=function(){}
SystemEx.TypedArrays.ArrayBuffer.create=function(length){return new ArrayBuffer(length);}
SystemEx.TypedArrays.ArrayBuffer.prototype={get_length:function(){return this.length;}}
SystemEx.TypedArrays.DataView=function(){SystemEx.TypedArrays.DataView.initializeBase(this);}
SystemEx.TypedArrays.DataView.prototype={getByte:function(byteOffset){return this.getUInt8(byteOffset);},getSByte:function(byteOffset){return this.getInt8(byteOffset);},getInt16:function(byteOffset,littleEndian){return this.getInt16(byteOffset, littleEndian);},getUInt16:function(byteOffset,littleEndian){return this.getUInt16(byteOffset, littleEndian);},getInt32:function(byteOffset,littleEndian){return this.getInt32(byteOffset, littleEndian);},getUInt32:function(byteOffset,littleEndian){return this.getUInt32(byteOffset, littleEndian);},getSingle:function(byteOffset,littleEndian){return this.getFloat(byteOffset, littleEndian);},getDouble:function(byteOffset,littleEndian){return this.getDouble(byteOffset, littleEndian);},setByte:function(byteOffset,value,littleEndian){this.setUint8(byteOffset, value, littleEndian);},setSByte:function(byteOffset,value,littleEndian){this.setInt8(byteOffset, value, littleEndian);},setInt16:function(byteOffset,value,littleEndian){this.setInt16(byteOffset, value, littleEndian);},setUint16:function(byteOffset,value,littleEndian){this.setUint16(byteOffset, value, littleEndian);},setInt32:function(byteOffset,value,littleEndian){this.setInt32(byteOffset, value, littleEndian);},setUint32:function(byteOffset,value,littleEndian){this.setUint32(byteOffset, value, littleEndian);},setSingle:function(byteOffset,value,littleEndian){this.setFloat(byteOffset, value, littleEndian);},setDouble:function(byteOffset,value,littleEndian){this.setDouble(byteOffset, value, littleEndian);}}
SystemEx.TypedArrays.Float64Array=function(){SystemEx.TypedArrays.Float64Array.initializeBase(this);}
SystemEx.TypedArrays.Float64Array.create=function(buffer){return new Float64Array(buffer);}
SystemEx.TypedArrays.Float64Array.create2=function(buffer,byteOffset){return new Float64Array(buffer, byteOffset);}
SystemEx.TypedArrays.Float64Array.create3=function(buffer,byteOffset,length){return new Float64Array(buffer, byteOffset, length);}
SystemEx.TypedArrays.Float64Array.createA=function(data){return SystemEx.TypedArrays.Float64Array.create6(SystemEx.JSConvertEx.doublesToJSArray(data));}
SystemEx.TypedArrays.Float64Array.create4=function(array){return new Float64Array(array);}
SystemEx.TypedArrays.Float64Array.create5=function(size){return new Float64Array(size);}
SystemEx.TypedArrays.Float64Array.create6=function(data){return new Float64Array(data);}
SystemEx.TypedArrays.Float64Array.prototype={get:function(index){return this[index];},get_length:function(){return this.length;},setA:function(array,offset){this.set5(SystemEx.JSConvertEx.doublesToJSArray(array),offset);},set:function(array){this.set(array);},set2:function(array,offset){this.set(array, offset);},set3:function(index,value){this[index] = value;},set4:function(array){this.set(array);},set5:function(array,offset){this.set(array, offset);},slice:function(offset,length){return this.slice(offset, length);}}
SystemEx.TypedArrays.Uint32Array=function(){SystemEx.TypedArrays.Uint32Array.initializeBase(this);}
SystemEx.TypedArrays.Uint32Array.create=function(buffer){return new Uint32Array(buffer);}
SystemEx.TypedArrays.Uint32Array.create2=function(buffer,byteOffset){return new Uint32Array(buffer, byteOffset);}
SystemEx.TypedArrays.Uint32Array.create3=function(buffer,byteOffset,length){return new Uint32Array(buffer, byteOffset, length);}
SystemEx.TypedArrays.Uint32Array.createA=function(data){return SystemEx.TypedArrays.Uint32Array.create6(SystemEx.JSConvertEx.ints32ToJSArray(data));}
SystemEx.TypedArrays.Uint32Array.create4=function(array){return new Uint32Array(array);}
SystemEx.TypedArrays.Uint32Array.create5=function(size){return new Uint32Array(size);}
SystemEx.TypedArrays.Uint32Array.create6=function(data){return new Uint32Array(data);}
SystemEx.TypedArrays.Uint32Array.prototype={get:function(index){return this[index];},get_length:function(){return this.length;},setA:function(array,offset){this.set5(SystemEx.JSConvertEx.ints32ToJSArray(array),offset);},set:function(array){this.set(array);},set2:function(array,offset){this.set(array, offset);},set3:function(index,value){this[index] = value;},set4:function(array){this.set(array);},set5:function(array,offset){this.set(array, offset);},slice:function(offset,length){return this.slice(offset, length);}}
SystemEx.TypedArrays.Int16Array=function(){SystemEx.TypedArrays.Int16Array.initializeBase(this);}
SystemEx.TypedArrays.Int16Array.create=function(buffer){return new Int16Array(buffer);}
SystemEx.TypedArrays.Int16Array.create2=function(buffer,byteOffset){return new Int16Array(buffer, byteOffset);}
SystemEx.TypedArrays.Int16Array.create3=function(buffer,byteOffset,length){return new Int16Array(buffer, byteOffset, length);}
SystemEx.TypedArrays.Int16Array.createA=function(data){return SystemEx.TypedArrays.Int16Array.create6(SystemEx.JSConvertEx.ints32ToJSArray(data));}
SystemEx.TypedArrays.Int16Array.create4=function(array){return new Int16Array(array);}
SystemEx.TypedArrays.Int16Array.create5=function(size){return new Int16Array(size);}
SystemEx.TypedArrays.Int16Array.create6=function(data){return new Int16Array(data);}
SystemEx.TypedArrays.Int16Array.prototype={get:function(index){return this[index];},get_length:function(){return this.length;},setA:function(array,offset){this.set5(SystemEx.JSConvertEx.ints32ToJSArray(array),offset);},set:function(array){this.set(array);},set2:function(array,offset){this.set(array, offset);},set3:function(index,value){this[index] = value;},set4:function(array){this.set(array);},set5:function(array,offset){this.set(array, offset);},slice:function(offset,length){return this.slice(offset, length);}}
SystemEx.TypedArrays.Uint16Array=function(){SystemEx.TypedArrays.Uint16Array.initializeBase(this);}
SystemEx.TypedArrays.Uint16Array.create=function(buffer){return new Uint16Array(buffer);}
SystemEx.TypedArrays.Uint16Array.create2=function(buffer,byteOffset){return new Uint16Array(buffer, byteOffset);}
SystemEx.TypedArrays.Uint16Array.create3=function(buffer,byteOffset,length){return new Uint16Array(buffer, byteOffset, length);}
SystemEx.TypedArrays.Uint16Array.createA=function(data){return SystemEx.TypedArrays.Uint16Array.create6(SystemEx.JSConvertEx.ints32ToJSArray(data));}
SystemEx.TypedArrays.Uint16Array.create4=function(array){return new Uint16Array(array);}
SystemEx.TypedArrays.Uint16Array.create5=function(size){return new Uint16Array(size);}
SystemEx.TypedArrays.Uint16Array.create6=function(data){return new Uint16Array(data);}
SystemEx.TypedArrays.Uint16Array.prototype={get:function(index){return this[index];},get_length:function(){return this.length;},setA:function(array,offset){this.set5(SystemEx.JSConvertEx.ints32ToJSArray(array),offset);},set:function(array){this.set(array);},set2:function(array,offset){this.set(array, offset);},set3:function(index,value){this[index] = value;},set4:function(array){this.set(array);},set5:function(array,offset){this.set(array, offset);},slice:function(offset,length){return this.slice(offset, length);}}
SystemEx.TypedArrays.Uint8Array=function(){SystemEx.TypedArrays.Uint8Array.initializeBase(this);}
SystemEx.TypedArrays.Uint8Array.create=function(buffer){return new Uint8Array(buffer);}
SystemEx.TypedArrays.Uint8Array.create2=function(buffer,byteOffset){return new Uint8Array(buffer, byteOffset);}
SystemEx.TypedArrays.Uint8Array.create3=function(buffer,byteOffset,length){return new Uint8Array(buffer, byteOffset, length);}
SystemEx.TypedArrays.Uint8Array.createA=function(data){return SystemEx.TypedArrays.Uint8Array.create6(SystemEx.JSConvertEx.ints32ToJSArray(data));}
SystemEx.TypedArrays.Uint8Array.create4=function(array){return new Uint8Array(array);}
SystemEx.TypedArrays.Uint8Array.create5=function(size){return new Uint8Array(size);}
SystemEx.TypedArrays.Uint8Array.create6=function(data){return new Uint8Array(data);}
SystemEx.TypedArrays.Uint8Array.prototype={get:function(index){return this[index];},get_length:function(){return this.length;},setA:function(array,offset){this.set5(SystemEx.JSConvertEx.ints32ToJSArray(array),offset);},set:function(array){this.set(array);},set2:function(array,offset){this.set(array, offset);},set3:function(index,value){this[index] = value;},set4:function(array){this.set(array);},set5:function(array,offset){this.set(array, offset);},slice:function(offset,length){return this.slice(offset, length);}}
SystemEx.TypedArrays.Float32Array=function(){SystemEx.TypedArrays.Float32Array.initializeBase(this);}
SystemEx.TypedArrays.Float32Array.create=function(buffer){return new Float32Array(buffer);}
SystemEx.TypedArrays.Float32Array.create2=function(buffer,byteOffset){return new Float32Array(buffer, byteOffset);}
SystemEx.TypedArrays.Float32Array.create3=function(buffer,byteOffset,length){return new Float32Array(buffer, byteOffset, length);}
SystemEx.TypedArrays.Float32Array.createA=function(data){return SystemEx.TypedArrays.Float32Array.create6(SystemEx.JSConvertEx.singlesToJSArray(data));}
SystemEx.TypedArrays.Float32Array.create4=function(array){return new Float32Array(array);}
SystemEx.TypedArrays.Float32Array.create5=function(size){return new Float32Array(size);}
SystemEx.TypedArrays.Float32Array.create6=function(data){return new Float32Array(data);}
SystemEx.TypedArrays.Float32Array.prototype={get:function(index){return this[index];},get_length:function(){return this.length;},setA:function(array,offset){this.set5(SystemEx.JSConvertEx.singlesToJSArray(array),offset);},set:function(array){this.set(array);},set2:function(array,offset){this.set(array, offset);},set3:function(index,value){this[index] = value;},set4:function(array){this.set(array);},set5:function(array,offset){this.set(array, offset);},slice:function(offset,length){return this.slice(offset, length);}}
SystemEx.TypedArrays.Int32Array=function(){SystemEx.TypedArrays.Int32Array.initializeBase(this);}
SystemEx.TypedArrays.Int32Array.create=function(buffer){return new Int32Array(buffer);}
SystemEx.TypedArrays.Int32Array.create2=function(buffer,byteOffset){return new Int32Array(buffer, byteOffset);}
SystemEx.TypedArrays.Int32Array.create3=function(buffer,byteOffset,length){return new Int32Array(buffer, byteOffset, length);}
SystemEx.TypedArrays.Int32Array.createA=function(data){return SystemEx.TypedArrays.Int32Array.create6(SystemEx.JSConvertEx.ints32ToJSArray(data));}
SystemEx.TypedArrays.Int32Array.create4=function(array){return new Int32Array(array);}
SystemEx.TypedArrays.Int32Array.create5=function(size){return new Int32Array(size);}
SystemEx.TypedArrays.Int32Array.create6=function(data){return new Int32Array(data);}
SystemEx.TypedArrays.Int32Array.prototype={get:function(index){return this[index];},get_length:function(){return this.length;},setA:function(array,offset){this.set5(SystemEx.JSConvertEx.ints32ToJSArray(array),offset);},set:function(array){this.set(array);},set2:function(array,offset){this.set(array, offset);},set3:function(index,value){this[index] = value;},set4:function(array){this.set(array);},set5:function(array,offset){this.set(array, offset);},slice:function(offset,length){return this.slice(offset, length);}}
SystemEx.TypedArrays.Int8Array=function(){SystemEx.TypedArrays.Int8Array.initializeBase(this);}
SystemEx.TypedArrays.Int8Array.create=function(buffer){return new Int8Array(buffer);}
SystemEx.TypedArrays.Int8Array.create2=function(buffer,byteOffset){return new Int8Array(buffer, byteOffset);}
SystemEx.TypedArrays.Int8Array.create3=function(buffer,byteOffset,length){return new Int8Array(buffer, byteOffset, length);}
SystemEx.TypedArrays.Int8Array.createA=function(data){return SystemEx.TypedArrays.Int8Array.create6(SystemEx.JSConvertEx.ints32ToJSArray(data));}
SystemEx.TypedArrays.Int8Array.create4=function(array){return new Int8Array(array);}
SystemEx.TypedArrays.Int8Array.create5=function(size){return new Int8Array(size);}
SystemEx.TypedArrays.Int8Array.create6=function(data){return new Int8Array(data);}
SystemEx.TypedArrays.Int8Array.prototype={get:function(index){return this[index];},get_length:function(){return this.length;},setA:function(array,offset){this.set5(SystemEx.JSConvertEx.ints32ToJSArray(array),offset);},set:function(array){this.set(array);},set2:function(array,offset){this.set(array, offset);},set3:function(index,value){this[index] = value;},set4:function(array){this.set(array);},set5:function(array,offset){this.set(array, offset);},slice:function(offset,length){return this.slice(offset, length);}}
SystemEx.JSArrayEx.registerClass('SystemEx.JSArrayEx');SystemEx.JSConvertEx.registerClass('SystemEx.JSConvertEx');SystemEx.JSArrayString.registerClass('SystemEx.JSArrayString');SystemEx.JSArrayNumber.registerClass('SystemEx.JSArrayNumber');SystemEx.JSArrayMixed.registerClass('SystemEx.JSArrayMixed');SystemEx.JSArrayInteger.registerClass('SystemEx.JSArrayInteger');SystemEx.JSArrayBoolean.registerClass('SystemEx.JSArrayBoolean');SystemEx.JSArray.registerClass('SystemEx.JSArray');SystemEx.StringBuilderEx.registerClass('SystemEx.StringBuilderEx');SystemEx.IO.Stream.registerClass('SystemEx.IO.Stream');SystemEx.IO.MemoryStream.registerClass('SystemEx.IO.MemoryStream',SystemEx.IO.Stream);SystemEx.IO.SE.registerClass('SystemEx.IO.SE');SystemEx.IO.FileInfo.registerClass('SystemEx.IO.FileInfo');SystemEx.IO.FileStream.registerClass('SystemEx.IO.FileStream',SystemEx.IO.Stream);SystemEx.IO.Path.registerClass('SystemEx.IO.Path');SystemEx.IO.Directory.registerClass('SystemEx.IO.Directory');SystemEx.IO.File.registerClass('SystemEx.IO.File');SystemEx.Html.WindowEx.registerClass('SystemEx.Html.WindowEx');SystemEx.Html.LocalStorage.registerClass('SystemEx.Html.LocalStorage');SystemEx.TypedArrays.ArrayBufferView.registerClass('SystemEx.TypedArrays.ArrayBufferView');SystemEx.TypedArrays.ArrayBuffer.registerClass('SystemEx.TypedArrays.ArrayBuffer');SystemEx.TypedArrays.DataView.registerClass('SystemEx.TypedArrays.DataView',SystemEx.TypedArrays.ArrayBufferView);SystemEx.TypedArrays.Float64Array.registerClass('SystemEx.TypedArrays.Float64Array',SystemEx.TypedArrays.ArrayBufferView);SystemEx.TypedArrays.Uint32Array.registerClass('SystemEx.TypedArrays.Uint32Array',SystemEx.TypedArrays.ArrayBufferView);SystemEx.TypedArrays.Int16Array.registerClass('SystemEx.TypedArrays.Int16Array',SystemEx.TypedArrays.ArrayBufferView);SystemEx.TypedArrays.Uint16Array.registerClass('SystemEx.TypedArrays.Uint16Array',SystemEx.TypedArrays.ArrayBufferView);SystemEx.TypedArrays.Uint8Array.registerClass('SystemEx.TypedArrays.Uint8Array',SystemEx.TypedArrays.ArrayBufferView);SystemEx.TypedArrays.Float32Array.registerClass('SystemEx.TypedArrays.Float32Array',SystemEx.TypedArrays.ArrayBufferView);SystemEx.TypedArrays.Int32Array.registerClass('SystemEx.TypedArrays.Int32Array',SystemEx.TypedArrays.ArrayBufferView);SystemEx.TypedArrays.Int8Array.registerClass('SystemEx.TypedArrays.Int8Array',SystemEx.TypedArrays.ArrayBufferView);SystemEx.JSConvertEx.$0=SystemEx.TypedArrays.Int8Array.create5(4);SystemEx.JSConvertEx.$1=SystemEx.TypedArrays.Int32Array.create3(SystemEx.JSConvertEx.$0.get_buffer(),0,1);SystemEx.JSConvertEx.$2=SystemEx.TypedArrays.Float32Array.create3(SystemEx.JSConvertEx.$0.get_buffer(),0,1);SystemEx.IO.FileInfo.separatorChar='/';SystemEx.IO.FileInfo.separator='//';SystemEx.IO.FileInfo.pathSeparatorChar=':';SystemEx.IO.FileInfo.pathSeparator=':';SystemEx.IO.FileInfo.root=new SystemEx.IO.FileInfo('');SystemEx.TypedArrays.Float64Array.byteS_PER_ELEMENT=4;SystemEx.TypedArrays.Uint32Array.byteS_PER_ELEMENT=4;SystemEx.TypedArrays.Int16Array.byteS_PER_ELEMENT=2;SystemEx.TypedArrays.Uint16Array.byteS_PER_ELEMENT=2;SystemEx.TypedArrays.Uint8Array.byteS_PER_ELEMENT=1;SystemEx.TypedArrays.Float32Array.byteS_PER_ELEMENT=4;SystemEx.TypedArrays.Int32Array.byteS_PER_ELEMENT=4;SystemEx.TypedArrays.Int8Array.byteS_PER_ELEMENT=1;}
ss.loader.registerScript('Script.WebEx',[],executeScript);})();// This script was generated using Script# v0.6.1.0
