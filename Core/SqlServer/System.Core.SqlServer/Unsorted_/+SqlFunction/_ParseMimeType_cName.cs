﻿//using Instinct;
////- UserDefinedFunctions -//
//public partial class UserDefinedFunctions
//{
//    [Microsoft.SqlServer.Server.SqlFunction(Name = "fn_ParseMimeType~cName")]
//    public static string ParseMimeType(string a, string x, string b)
//    {
//    }
//};

//CREATE Function [dbo].[fn_ParseMimeType~cName](@cName nvarchar(500)) Returns nvarchar(100) As Begin
//   --[Instinct.Attribute.ApplicationTypeVersion("1.0")]
//   --+ [MimeType Source]http://www.webmaster-toolkit.com/mime-types.shtml
//   Declare @nIndex int; Set @nIndex = CharIndex(N'.', @cName)
//   If (@nIndex = 0) Begin
//      Return Null;
//   End
//   Declare @cExtension nvarchar(50); Set @cExtension = Right(@cName, Len(@cName) - @nIndex);
//   Declare @cMimeType nvarchar(100);
//   --+ decode mime-type
//   Select @cMimeType = Case @cExtension
//   When N'3dm' Then N'x-world/x-3dmf'
//   When N'3dmf' Then N'x-world/x-3dmf'
//   When N'a' Then N'application/octet-stream'
//   When N'aab' Then N'application/x-authorware-bin'
//   When N'aam' Then N'application/x-authorware-map'
//   When N'aas' Then N'application/x-authorware-seg'
//   When N'abc' Then N'text/vnd.abc'
//   When N'acgi' Then N'text/html'
//   When N'afl' Then N'video/animaflex'
//   When N'ai' Then N'application/postscript'
//   When N'aif' Then N'audio/aiff'
//   When N'aif' Then N'audio/x-aiff'
//   When N'aifc' Then N'audio/aiff'
//   When N'aifc' Then N'audio/x-aiff'
//   When N'aiff' Then N'audio/aiff'
//   When N'aiff' Then N'audio/x-aiff'
//   When N'aim' Then N'application/x-aim'
//   When N'aip' Then N'text/x-audiosoft-intra'
//   When N'ani' Then N'application/x-navi-animation'
//   When N'aos' Then N'application/x-nokia-9000-communicator-add-on-software'
//   When N'aps' Then N'application/mime'
//   When N'arc' Then N'application/octet-stream'
//   When N'arj' Then N'application/arj'
//   When N'arj' Then N'application/octet-stream'
//   When N'art' Then N'image/x-jg'
//   When N'asf' Then N'video/x-ms-asf'
//   When N'asm' Then N'text/x-asm'
//   When N'asp' Then N'text/asp'
//   When N'asx' Then N'application/x-mplayer2'
//   When N'asx' Then N'video/x-ms-asf'
//   When N'asx' Then N'video/x-ms-asf-plugin'
//   When N'au' Then N'audio/basic'
//   When N'au' Then N'audio/x-au'
//   When N'avi' Then N'application/x-troff-msvideo'
//   When N'avi' Then N'video/avi'
//   When N'avi' Then N'video/msvideo'
//   When N'avi' Then N'video/x-msvideo'
//   When N'avs' Then N'video/avs-video'
//   When N'bcpio' Then N'application/x-bcpio'
//   When N'bin' Then N'application/mac-binary'
//   When N'bin' Then N'application/macbinary'
//   When N'bin' Then N'application/octet-stream'
//   When N'bin' Then N'application/x-binary'
//   When N'bin' Then N'application/x-macbinary'
//   When N'bm' Then N'image/bmp'
//   When N'bmp' Then N'image/bmp'
//   When N'bmp' Then N'image/x-windows-bmp'
//   When N'boo' Then N'application/book'
//   When N'book' Then N'application/book'
//   When N'boz' Then N'application/x-bzip2'
//   When N'bsh' Then N'application/x-bsh'
//   When N'bz' Then N'application/x-bzip'
//   When N'bz2' Then N'application/x-bzip2'
//   When N'c' Then N'text/plain'
//   When N'c' Then N'text/x-c'
//   When N'c++' Then N'text/plain'
//   When N'cat' Then N'application/vnd.ms-pki.seccat'
//   When N'cc' Then N'text/plain'
//   When N'cc' Then N'text/x-c'
//   When N'ccad' Then N'application/clariscad'
//   When N'cco' Then N'application/x-cocoa'
//   When N'cdf' Then N'application/cdf'
//   When N'cdf' Then N'application/x-cdf'
//   When N'cdf' Then N'application/x-netcdf'
//   When N'cer' Then N'application/pkix-cert'
//   When N'cer' Then N'application/x-x509-ca-cert'
//   When N'cha' Then N'application/x-chat'
//   When N'chat' Then N'application/x-chat'
//   When N'class' Then N'application/java'
//   When N'class' Then N'application/java-byte-code'
//   When N'class' Then N'application/x-java-class'
//   When N'com' Then N'application/octet-stream'
//   When N'com' Then N'text/plain'
//   When N'conf' Then N'text/plain'
//   When N'cpio' Then N'application/x-cpio'
//   When N'cpp' Then N'text/x-c'
//   When N'cpt' Then N'application/mac-compactpro'
//   When N'cpt' Then N'application/x-compactpro'
//   When N'cpt' Then N'application/x-cpt'
//   When N'crl' Then N'application/pkcs-crl'
//   When N'crl' Then N'application/pkix-crl'
//   When N'crt' Then N'application/pkix-cert'
//   When N'crt' Then N'application/x-x509-ca-cert'
//   When N'crt' Then N'application/x-x509-user-cert'
//   When N'csh' Then N'application/x-csh'
//   When N'csh' Then N'text/x-script.csh'
//   When N'css' Then N'application/x-pointplus'
//   When N'css' Then N'text/css'
//   When N'cxx' Then N'text/plain'
//   When N'dcr' Then N'application/x-director'
//   When N'deepv' Then N'application/x-deepv'
//   When N'def' Then N'text/plain'
//   When N'der' Then N'application/x-x509-ca-cert'
//   When N'dif' Then N'video/x-dv'
//   When N'dir' Then N'application/x-director'
//   When N'dl' Then N'video/dl'
//   When N'dl' Then N'video/x-dl'
//   When N'doc' Then N'application/msword'
//   When N'dot' Then N'application/msword'
//   When N'dp' Then N'application/commonground'
//   When N'drw' Then N'application/drafting'
//   When N'dump' Then N'application/octet-stream'
//   When N'dv' Then N'video/x-dv'
//   When N'dvi' Then N'application/x-dvi'
//   When N'dwf' Then N'drawing/x-dwf (old)'
//   When N'dwf' Then N'model/vnd.dwf'
//   When N'dwg' Then N'application/acad'
//   When N'dwg' Then N'image/vnd.dwg'
//   When N'dwg' Then N'image/x-dwg'
//   When N'dxf' Then N'application/dxf'
//   When N'dxf' Then N'image/vnd.dwg'
//   When N'dxf' Then N'image/x-dwg'
//   When N'dxr' Then N'application/x-director'
//   When N'el' Then N'text/x-script.elisp'
//   When N'elc' Then N'application/x-bytecode.elisp (compiled elisp)'
//   When N'elc' Then N'application/x-elc'
//   When N'env' Then N'application/x-envoy'
//   When N'eps' Then N'application/postscript'
//   When N'es' Then N'application/x-esrehber'
//   When N'etx' Then N'text/x-setext'
//   When N'evy' Then N'application/envoy'
//   When N'evy' Then N'application/x-envoy'
//   When N'exe' Then N'application/octet-stream'
//   When N'f' Then N'text/plain'
//   When N'f' Then N'text/x-fortran'
//   When N'f77' Then N'text/x-fortran'
//   When N'f90' Then N'text/plain'
//   When N'f90' Then N'text/x-fortran'
//   When N'fdf' Then N'application/vnd.fdf'
//   When N'fif' Then N'application/fractals'
//   When N'fif' Then N'image/fif'
//   When N'fli' Then N'video/fli'
//   When N'fli' Then N'video/x-fli'
//   When N'flo' Then N'image/florian'
//   When N'flx' Then N'text/vnd.fmi.flexstor'
//   When N'fmf' Then N'video/x-atomic3d-feature'
//   When N'for' Then N'text/plain'
//   When N'for' Then N'text/x-fortran'
//   When N'fpx' Then N'image/vnd.fpx'
//   When N'fpx' Then N'image/vnd.net-fpx'
//   When N'frl' Then N'application/freeloader'
//   When N'funk' Then N'audio/make'
//   When N'g' Then N'text/plain'
//   When N'g3' Then N'image/g3fax'
//   When N'gif' Then N'image/gif'
//   When N'gl' Then N'video/gl'
//   When N'gl' Then N'video/x-gl'
//   When N'gsd' Then N'audio/x-gsm'
//   When N'gsm' Then N'audio/x-gsm'
//   When N'gsp' Then N'application/x-gsp'
//   When N'gss' Then N'application/x-gss'
//   When N'gtar' Then N'application/x-gtar'
//   When N'gz' Then N'application/x-compressed'
//   When N'gz' Then N'application/x-gzip'
//   When N'gzip' Then N'application/x-gzip'
//   When N'gzip' Then N'multipart/x-gzip'
//   When N'h' Then N'text/plain'
//   When N'h' Then N'text/x-h'
//   When N'hdf' Then N'application/x-hdf'
//   When N'help' Then N'application/x-helpfile'
//   When N'hgl' Then N'application/vnd.hp-hpgl'
//   When N'hh' Then N'text/plain'
//   When N'hh' Then N'text/x-h'
//   When N'hlb' Then N'text/x-script'
//   When N'hlp' Then N'application/hlp'
//   When N'hlp' Then N'application/x-helpfile'
//   When N'hlp' Then N'application/x-winhelp'
//   When N'hpg' Then N'application/vnd.hp-hpgl'
//   When N'hpgl' Then N'application/vnd.hp-hpgl'
//   When N'hqx' Then N'application/binhex'
//   When N'hqx' Then N'application/binhex4'
//   When N'hqx' Then N'application/mac-binhex'
//   When N'hqx' Then N'application/mac-binhex40'
//   When N'hqx' Then N'application/x-binhex40'
//   When N'hqx' Then N'application/x-mac-binhex40'
//   When N'hta' Then N'application/hta'
//   When N'htc' Then N'text/x-component'
//   When N'htm' Then N'text/html'
//   When N'html' Then N'text/html'
//   When N'htmls' Then N'text/html'
//   When N'htt' Then N'text/webviewhtml'
//   When N'htx' Then N'text/html'
//   When N'ice' Then N'x-conference/x-cooltalk'
//   When N'ico' Then N'image/x-icon'
//   When N'idc' Then N'text/plain'
//   When N'ief' Then N'image/ief'
//   When N'iefs' Then N'image/ief'
//   When N'iges' Then N'application/iges'
//   When N'iges' Then N'model/iges'
//   When N'igs' Then N'application/iges'
//   When N'igs' Then N'model/iges'
//   When N'ima' Then N'application/x-ima'
//   When N'imap' Then N'application/x-httpd-imap'
//   When N'inf' Then N'application/inf'
//   When N'ins' Then N'application/x-internett-signup'
//   When N'ip' Then N'application/x-ip2'
//   When N'isu' Then N'video/x-isvideo'
//   When N'it' Then N'audio/it'
//   When N'iv' Then N'application/x-inventor'
//   When N'ivr' Then N'i-world/i-vrml'
//   When N'ivy' Then N'application/x-livescreen'
//   When N'jam' Then N'audio/x-jam'
//   When N'jav' Then N'text/plain'
//   When N'jav' Then N'text/x-java-source'
//   When N'java' Then N'text/plain'
//   When N'java' Then N'text/x-java-source'
//   When N'jcm' Then N'application/x-java-commerce'
//   When N'jfif' Then N'image/jpeg'
//   When N'jfif' Then N'image/pjpeg'
//   When N'jfif-tbnl' Then N'image/jpeg'
//   When N'jpe' Then N'image/jpeg'
//   When N'jpe' Then N'image/pjpeg'
//   When N'jpeg' Then N'image/jpeg'
//   When N'jpeg' Then N'image/pjpeg'
//   When N'jpg' Then N'image/jpeg'
//   When N'jpg' Then N'image/pjpeg'
//   When N'jps' Then N'image/x-jps'
//   When N'js' Then N'application/x-javascript'
//   When N'jut' Then N'image/jutvision'
//   When N'kar' Then N'audio/midi'
//   When N'kar' Then N'music/x-karaoke'
//   When N'ksh' Then N'application/x-ksh'
//   When N'ksh' Then N'text/x-script.ksh'
//   When N'la' Then N'audio/nspaudio'
//   When N'la' Then N'audio/x-nspaudio'
//   When N'lam' Then N'audio/x-liveaudio'
//   When N'latex' Then N'application/x-latex'
//   When N'lha' Then N'application/lha'
//   When N'lha' Then N'application/octet-stream'
//   When N'lha' Then N'application/x-lha'
//   When N'lhx' Then N'application/octet-stream'
//   When N'list' Then N'text/plain'
//   When N'lma' Then N'audio/nspaudio'
//   When N'lma' Then N'audio/x-nspaudio'
//   When N'log' Then N'text/plain'
//   When N'lsp' Then N'application/x-lisp'
//   When N'lsp' Then N'text/x-script.lisp'
//   When N'lst' Then N'text/plain'
//   When N'lsx' Then N'text/x-la-asf'
//   When N'ltx' Then N'application/x-latex'
//   When N'lzh' Then N'application/octet-stream'
//   When N'lzh' Then N'application/x-lzh'
//   When N'lzx' Then N'application/lzx'
//   When N'lzx' Then N'application/octet-stream'
//   When N'lzx' Then N'application/x-lzx'
//   When N'm' Then N'text/plain'
//   When N'm' Then N'text/x-m'
//   When N'm1v' Then N'video/mpeg'
//   When N'm2a' Then N'audio/mpeg'
//   When N'm2v' Then N'video/mpeg'
//   When N'm3u' Then N'audio/x-mpequrl'
//   When N'man' Then N'application/x-troff-man'
//   When N'map' Then N'application/x-navimap'
//   When N'mar' Then N'text/plain'
//   When N'mbd' Then N'application/mbedlet'
//   When N'mc$' Then N'application/x-magic-cap-package-1.0'
//   When N'mcd' Then N'application/mcad'
//   When N'mcd' Then N'application/x-mathcad'
//   When N'mcf' Then N'image/vasa'
//   When N'mcf' Then N'text/mcf'
//   When N'mcp' Then N'application/netmc'
//   When N'me' Then N'application/x-troff-me'
//   When N'mht' Then N'message/rfc822'
//   When N'mhtml' Then N'message/rfc822'
//   When N'mid' Then N'application/x-midi'
//   When N'mid' Then N'audio/midi'
//   When N'mid' Then N'audio/x-mid'
//   When N'mid' Then N'audio/x-midi'
//   When N'mid' Then N'music/crescendo'
//   When N'mid' Then N'x-music/x-midi'
//   When N'midi' Then N'application/x-midi'
//   When N'midi' Then N'audio/midi'
//   When N'midi' Then N'audio/x-mid'
//   When N'midi' Then N'audio/x-midi'
//   When N'midi' Then N'music/crescendo'
//   When N'midi' Then N'x-music/x-midi'
//   When N'mif' Then N'application/x-frame'
//   When N'mif' Then N'application/x-mif'
//   When N'mime' Then N'message/rfc822'
//   When N'mime' Then N'www/mime'
//   When N'mjf' Then N'audio/x-vnd.audioexplosion.mjuicemediafile'
//   When N'mjpg' Then N'video/x-motion-jpeg'
//   When N'mm' Then N'application/base64'
//   When N'mm' Then N'application/x-meme'
//   When N'mme' Then N'application/base64'
//   When N'mod' Then N'audio/mod'
//   When N'mod' Then N'audio/x-mod'
//   When N'moov' Then N'video/quicktime'
//   When N'mov' Then N'video/quicktime'
//   When N'movie' Then N'video/x-sgi-movie'
//   When N'mp2' Then N'audio/mpeg'
//   When N'mp2' Then N'audio/x-mpeg'
//   When N'mp2' Then N'video/mpeg'
//   When N'mp2' Then N'video/x-mpeg'
//   When N'mp2' Then N'video/x-mpeq2a'
//   When N'mp3' Then N'audio/mpeg3'
//   When N'mp3' Then N'audio/x-mpeg-3'
//   When N'mp3' Then N'video/mpeg'
//   When N'mp3' Then N'video/x-mpeg'
//   When N'mpa' Then N'audio/mpeg'
//   When N'mpa' Then N'video/mpeg'
//   When N'mpc' Then N'application/x-project'
//   When N'mpe' Then N'video/mpeg'
//   When N'mpeg' Then N'video/mpeg'
//   When N'mpg' Then N'audio/mpeg'
//   When N'mpg' Then N'video/mpeg'
//   When N'mpga' Then N'audio/mpeg'
//   When N'mpp' Then N'application/vnd.ms-project'
//   When N'mpt' Then N'application/x-project'
//   When N'mpv' Then N'application/x-project'
//   When N'mpx' Then N'application/x-project'
//   When N'mrc' Then N'application/marc'
//   When N'ms' Then N'application/x-troff-ms'
//   When N'mv' Then N'video/x-sgi-movie'
//   When N'my' Then N'audio/make'
//   When N'mzz' Then N'application/x-vnd.audioexplosion.mzz'
//   When N'nap' Then N'image/naplps'
//   When N'naplps' Then N'image/naplps'
//   When N'nc' Then N'application/x-netcdf'
//   When N'ncm' Then N'application/vnd.nokia.configuration-message'
//   When N'nif' Then N'image/x-niff'
//   When N'niff' Then N'image/x-niff'
//   When N'nix' Then N'application/x-mix-transfer'
//   When N'nsc' Then N'application/x-conference'
//   When N'nvd' Then N'application/x-navidoc'
//   When N'o' Then N'application/octet-stream'
//   When N'oda' Then N'application/oda'
//   When N'omc' Then N'application/x-omc'
//   When N'omcd' Then N'application/x-omcdatamaker'
//   When N'omcr' Then N'application/x-omcregerator'
//   When N'p' Then N'text/x-pascal'
//   When N'p10' Then N'application/pkcs10'
//   When N'p10' Then N'application/x-pkcs10'
//   When N'p12' Then N'application/pkcs-12'
//   When N'p12' Then N'application/x-pkcs12'
//   When N'p7a' Then N'application/x-pkcs7-signature'
//   When N'p7c' Then N'application/pkcs7-mime'
//   When N'p7c' Then N'application/x-pkcs7-mime'
//   When N'p7m' Then N'application/pkcs7-mime'
//   When N'p7m' Then N'application/x-pkcs7-mime'
//   When N'p7r' Then N'application/x-pkcs7-certreqresp'
//   When N'p7s' Then N'application/pkcs7-signature'
//   When N'part' Then N'application/pro_eng'
//   When N'pas' Then N'text/pascal'
//   When N'pbm' Then N'image/x-portable-bitmap'
//   When N'pcl' Then N'application/vnd.hp-pcl'
//   When N'pcl' Then N'application/x-pcl'
//   When N'pct' Then N'image/x-pict'
//   When N'pcx' Then N'image/x-pcx'
//   When N'pdb' Then N'chemical/x-pdb'
//   When N'pdf' Then N'application/pdf'
//   When N'pfunk' Then N'audio/make'
//   When N'pfunk' Then N'audio/make.my.funk'
//   When N'pgm' Then N'image/x-portable-graymap'
//   When N'pgm' Then N'image/x-portable-greymap'
//   When N'pic' Then N'image/pict'
//   When N'pict' Then N'image/pict'
//   When N'pkg' Then N'application/x-newton-compatible-pkg'
//   When N'pko' Then N'application/vnd.ms-pki.pko'
//   When N'pl' Then N'text/plain'
//   When N'pl' Then N'text/x-script.perl'
//   When N'plx' Then N'application/x-pixclscript'
//   When N'pm' Then N'image/x-xpixmap'
//   When N'pm' Then N'text/x-script.perl-module'
//   When N'pm4' Then N'application/x-pagemaker'
//   When N'pm5' Then N'application/x-pagemaker'
//   When N'png' Then N'image/png'
//   When N'pnm' Then N'application/x-portable-anymap'
//   When N'pnm' Then N'image/x-portable-anymap'
//   When N'pot' Then N'application/mspowerpoint'
//   When N'pot' Then N'application/vnd.ms-powerpoint'
//   When N'pov' Then N'model/x-pov'
//   When N'ppa' Then N'application/vnd.ms-powerpoint'
//   When N'ppm' Then N'image/x-portable-pixmap'
//   When N'pps' Then N'application/mspowerpoint'
//   When N'pps' Then N'application/vnd.ms-powerpoint'
//   When N'ppt' Then N'application/mspowerpoint'
//   When N'ppt' Then N'application/powerpoint'
//   When N'ppt' Then N'application/vnd.ms-powerpoint'
//   When N'ppt' Then N'application/x-mspowerpoint'
//   When N'ppz' Then N'application/mspowerpoint'
//   When N'pre' Then N'application/x-freelance'
//   When N'prt' Then N'application/pro_eng'
//   When N'ps' Then N'application/postscript'
//   When N'psd' Then N'application/octet-stream'
//   When N'pvu' Then N'paleovu/x-pv'
//   When N'pwz' Then N'application/vnd.ms-powerpoint'
//   When N'py' Then N'text/x-script.phyton'
//   When N'pyc' Then N'applicaiton/x-bytecode.python'
//   When N'qcp' Then N'audio/vnd.qcelp'
//   When N'qd3' Then N'x-world/x-3dmf'
//   When N'qd3d' Then N'x-world/x-3dmf'
//   When N'qif' Then N'image/x-quicktime'
//   When N'qt' Then N'video/quicktime'
//   When N'qtc' Then N'video/x-qtc'
//   When N'qti' Then N'image/x-quicktime'
//   When N'qtif' Then N'image/x-quicktime'
//   When N'ra' Then N'audio/x-pn-realaudio'
//   When N'ra' Then N'audio/x-pn-realaudio-plugin'
//   When N'ra' Then N'audio/x-realaudio'
//   When N'ram' Then N'audio/x-pn-realaudio'
//   When N'ras' Then N'application/x-cmu-raster'
//   When N'ras' Then N'image/cmu-raster'
//   When N'ras' Then N'image/x-cmu-raster'
//   When N'rast' Then N'image/cmu-raster'
//   When N'rexx' Then N'text/x-script.rexx'
//   When N'rf' Then N'image/vnd.rn-realflash'
//   When N'rgb' Then N'image/x-rgb'
//   When N'rm' Then N'application/vnd.rn-realmedia'
//   When N'rm' Then N'audio/x-pn-realaudio'
//   When N'rmi' Then N'audio/mid'
//   When N'rmm' Then N'audio/x-pn-realaudio'
//   When N'rmp' Then N'audio/x-pn-realaudio'
//   When N'rmp' Then N'audio/x-pn-realaudio-plugin'
//   When N'rng' Then N'application/ringing-tones'
//   When N'rng' Then N'application/vnd.nokia.ringing-tone'
//   When N'rnx' Then N'application/vnd.rn-realplayer'
//   When N'roff' Then N'application/x-troff'
//   When N'rp' Then N'image/vnd.rn-realpix'
//   When N'rpm' Then N'audio/x-pn-realaudio-plugin'
//   When N'rt' Then N'text/richtext'
//   When N'rt' Then N'text/vnd.rn-realtext'
//   When N'rtf' Then N'application/rtf'
//   When N'rtf' Then N'application/x-rtf'
//   When N'rtf' Then N'text/richtext'
//   When N'rtx' Then N'application/rtf'
//   When N'rtx' Then N'text/richtext'
//   When N'rv' Then N'video/vnd.rn-realvideo'
//   When N's' Then N'text/x-asm'
//   When N's3m' Then N'audio/s3m'
//   When N'saveme' Then N'application/octet-stream'
//   When N'sbk' Then N'application/x-tbook'
//   When N'scm' Then N'application/x-lotusscreencam'
//   When N'scm' Then N'text/x-script.guile'
//   When N'scm' Then N'text/x-script.scheme'
//   When N'scm' Then N'video/x-scm'
//   When N'sdml' Then N'text/plain'
//   When N'sdp' Then N'application/sdp'
//   When N'sdp' Then N'application/x-sdp'
//   When N'sdr' Then N'application/sounder'
//   When N'sea' Then N'application/sea'
//   When N'sea' Then N'application/x-sea'
//   When N'set' Then N'application/set'
//   When N'sgm' Then N'text/sgml'
//   When N'sgm' Then N'text/x-sgml'
//   When N'sgml' Then N'text/sgml'
//   When N'sgml' Then N'text/x-sgml'
//   When N'sh' Then N'application/x-bsh'
//   When N'sh' Then N'application/x-sh'
//   When N'sh' Then N'application/x-shar'
//   When N'sh' Then N'text/x-script.sh'
//   When N'shar' Then N'application/x-bsh'
//   When N'shar' Then N'application/x-shar'
//   When N'shtml' Then N'text/html'
//   When N'shtml' Then N'text/x-server-parsed-html'
//   When N'sid' Then N'audio/x-psid'
//   When N'sit' Then N'application/x-sit'
//   When N'sit' Then N'application/x-stuffit'
//   When N'skd' Then N'application/x-koan'
//   When N'skm' Then N'application/x-koan'
//   When N'skp' Then N'application/x-koan'
//   When N'skt' Then N'application/x-koan'
//   When N'sl' Then N'application/x-seelogo'
//   When N'smi' Then N'application/smil'
//   When N'smil' Then N'application/smil'
//   When N'snd' Then N'audio/basic'
//   When N'snd' Then N'audio/x-adpcm'
//   When N'sol' Then N'application/solids'
//   When N'spc' Then N'application/x-pkcs7-certificates'
//   When N'spc' Then N'text/x-speech'
//   When N'spl' Then N'application/futuresplash'
//   When N'spr' Then N'application/x-sprite'
//   When N'sprite' Then N'application/x-sprite'
//   When N'src' Then N'application/x-wais-source'
//   When N'ssi' Then N'text/x-server-parsed-html'
//   When N'ssm' Then N'application/streamingmedia'
//   When N'sst' Then N'application/vnd.ms-pki.certstore'
//   When N'step' Then N'application/step'
//   When N'stl' Then N'application/sla'
//   When N'stl' Then N'application/vnd.ms-pki.stl'
//   When N'stl' Then N'application/x-navistyle'
//   When N'stp' Then N'application/step'
//   When N'sv4cpio' Then N'application/x-sv4cpio'
//   When N'sv4crc' Then N'application/x-sv4crc'
//   When N'svf' Then N'image/vnd.dwg'
//   When N'svf' Then N'image/x-dwg'
//   When N'svr' Then N'application/x-world'
//   When N'svr' Then N'x-world/x-svr'
//   When N'swf' Then N'application/x-shockwave-flash'
//   When N't' Then N'application/x-troff'
//   When N'talk' Then N'text/x-speech'
//   When N'tar' Then N'application/x-tar'
//   When N'tbk' Then N'application/toolbook'
//   When N'tbk' Then N'application/x-tbook'
//   When N'tcl' Then N'application/x-tcl'
//   When N'tcl' Then N'text/x-script.tcl'
//   When N'tcsh' Then N'text/x-script.tcsh'
//   When N'tex' Then N'application/x-tex'
//   When N'texi' Then N'application/x-texinfo'
//   When N'texinfo' Then N'application/x-texinfo'
//   When N'text' Then N'application/plain'
//   When N'text' Then N'text/plain'
//   When N'tgz' Then N'application/gnutar'
//   When N'tgz' Then N'application/x-compressed'
//   When N'tif' Then N'image/tiff'
//   When N'tif' Then N'image/x-tiff'
//   When N'tiff' Then N'image/tiff'
//   When N'tiff' Then N'image/x-tiff'
//   When N'tr' Then N'application/x-troff'
//   When N'tsi' Then N'audio/tsp-audio'
//   When N'tsp' Then N'application/dsptype'
//   When N'tsp' Then N'audio/tsplayer'
//   When N'tsv' Then N'text/tab-separated-values'
//   When N'turbot' Then N'image/florian'
//   When N'txt' Then N'text/plain'
//   When N'uil' Then N'text/x-uil'
//   When N'uni' Then N'text/uri-list'
//   When N'unis' Then N'text/uri-list'
//   When N'unv' Then N'application/i-deas'
//   When N'uri' Then N'text/uri-list'
//   When N'uris' Then N'text/uri-list'
//   When N'ustar' Then N'application/x-ustar'
//   When N'ustar' Then N'multipart/x-ustar'
//   When N'uu' Then N'application/octet-stream'
//   When N'uu' Then N'text/x-uuencode'
//   When N'uue' Then N'text/x-uuencode'
//   When N'vcd' Then N'application/x-cdlink'
//   When N'vcs' Then N'text/x-vcalendar'
//   When N'vda' Then N'application/vda'
//   When N'vdo' Then N'video/vdo'
//   When N'vew' Then N'application/groupwise'
//   When N'viv' Then N'video/vivo'
//   When N'viv' Then N'video/vnd.vivo'
//   When N'vivo' Then N'video/vivo'
//   When N'vivo' Then N'video/vnd.vivo'
//   When N'vmd' Then N'application/vocaltec-media-desc'
//   When N'vmf' Then N'application/vocaltec-media-file'
//   When N'voc' Then N'audio/voc'
//   When N'voc' Then N'audio/x-voc'
//   When N'vos' Then N'video/vosaic'
//   When N'vox' Then N'audio/voxware'
//   When N'vqe' Then N'audio/x-twinvq-plugin'
//   When N'vqf' Then N'audio/x-twinvq'
//   When N'vql' Then N'audio/x-twinvq-plugin'
//   When N'vrml' Then N'application/x-vrml'
//   When N'vrml' Then N'model/vrml'
//   When N'vrml' Then N'x-world/x-vrml'
//   When N'vrt' Then N'x-world/x-vrt'
//   When N'vsd' Then N'application/x-visio'
//   When N'vst' Then N'application/x-visio'
//   When N'vsw' Then N'application/x-visio'
//   When N'w60' Then N'application/wordperfect6.0'
//   When N'w61' Then N'application/wordperfect6.1'
//   When N'w6w' Then N'application/msword'
//   When N'wav' Then N'audio/wav'
//   When N'wav' Then N'audio/x-wav'
//   When N'wb1' Then N'application/x-qpro'
//   When N'wbmp' Then N'image/vnd.wap.wbmp'
//   When N'web' Then N'application/vnd.xara'
//   When N'wiz' Then N'application/msword'
//   When N'wk1' Then N'application/x-123'
//   When N'wmf' Then N'windows/metafile'
//   When N'wml' Then N'text/vnd.wap.wml'
//   When N'wmlc' Then N'application/vnd.wap.wmlc'
//   When N'wmls' Then N'text/vnd.wap.wmlscript'
//   When N'wmlsc' Then N'application/vnd.wap.wmlscriptc'
//   When N'word' Then N'application/msword'
//   When N'wp' Then N'application/wordperfect'
//   When N'wp5' Then N'application/wordperfect'
//   When N'wp5' Then N'application/wordperfect6.0'
//   When N'wp6' Then N'application/wordperfect'
//   When N'wpd' Then N'application/wordperfect'
//   When N'wpd' Then N'application/x-wpwin'
//   When N'wq1' Then N'application/x-lotus'
//   When N'wri' Then N'application/mswrite'
//   When N'wri' Then N'application/x-wri'
//   When N'wrl' Then N'application/x-world'
//   When N'wrl' Then N'model/vrml'
//   When N'wrl' Then N'x-world/x-vrml'
//   When N'wrz' Then N'model/vrml'
//   When N'wrz' Then N'x-world/x-vrml'
//   When N'wsc' Then N'text/scriplet'
//   When N'wsrc' Then N'application/x-wais-source'
//   When N'wtk' Then N'application/x-wintalk'
//   When N'xbm' Then N'image/x-xbitmap'
//   When N'xbm' Then N'image/x-xbm'
//   When N'xbm' Then N'image/xbm'
//   When N'xdr' Then N'video/x-amt-demorun'
//   When N'xgz' Then N'xgl/drawing'
//   When N'xif' Then N'image/vnd.xiff'
//   When N'xl' Then N'application/excel'
//   When N'xla' Then N'application/excel'
//   When N'xla' Then N'application/x-excel'
//   When N'xla' Then N'application/x-msexcel'
//   When N'xlb' Then N'application/excel'
//   When N'xlb' Then N'application/vnd.ms-excel'
//   When N'xlb' Then N'application/x-excel'
//   When N'xlc' Then N'application/excel'
//   When N'xlc' Then N'application/vnd.ms-excel'
//   When N'xlc' Then N'application/x-excel'
//   When N'xld' Then N'application/excel'
//   When N'xld' Then N'application/x-excel'
//   When N'xlk' Then N'application/excel'
//   When N'xlk' Then N'application/x-excel'
//   When N'xll' Then N'application/excel'
//   When N'xll' Then N'application/vnd.ms-excel'
//   When N'xll' Then N'application/x-excel'
//   When N'xlm' Then N'application/excel'
//   When N'xlm' Then N'application/vnd.ms-excel'
//   When N'xlm' Then N'application/x-excel'
//   When N'xls' Then N'application/excel'
//   When N'xls' Then N'application/vnd.ms-excel'
//   When N'xls' Then N'application/x-excel'
//   When N'xls' Then N'application/x-msexcel'
//   When N'xlt' Then N'application/excel'
//   When N'xlt' Then N'application/x-excel'
//   When N'xlv' Then N'application/excel'
//   When N'xlv' Then N'application/x-excel'
//   When N'xlw' Then N'application/excel'
//   When N'xlw' Then N'application/vnd.ms-excel'
//   When N'xlw' Then N'application/x-excel'
//   When N'xlw' Then N'application/x-msexcel'
//   When N'xm' Then N'audio/xm'
//   When N'xml' Then N'application/xml'
//   When N'xml' Then N'text/xml'
//   When N'xmz' Then N'xgl/movie'
//   When N'xpix' Then N'application/x-vnd.ls-xpix'
//   When N'xpm' Then N'image/x-xpixmap'
//   When N'xpm' Then N'image/xpm'
//   When N'x-png' Then N'image/png'
//   When N'xsr' Then N'video/x-amt-showrun'
//   When N'xwd' Then N'image/x-xwd'
//   When N'xwd' Then N'image/x-xwindowdump'
//   When N'xyz' Then N'chemical/x-pdb'
//   When N'z' Then N'application/x-compress'
//   When N'z' Then N'application/x-compressed'
//   When N'zip' Then N'application/x-compressed'
//   When N'zip' Then N'application/x-zip-compressed'
//   When N'zip' Then N'application/zip'
//   When N'zip' Then N'multipart/x-zip'
//   When N'zoo' Then N'application/octet-stream'
//   When N'zsh' Then N'text/x-script.zsh'
//   End
//   Return @cMimeType
//End
//GO
