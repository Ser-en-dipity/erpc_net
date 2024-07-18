/*
 * AUTOMATICALLY GENERATED FILE - DO NOT EDIT
 */

// Generated from ..\src\templates\dotnet_coders.template
extern const char * const kDotnetCoders;

const char * const kDotnetCoders = 
"{##################################################################################}\n"
"{################################### DECODING #####################################}\n"
"{##################################################################################}\n"
"\n"
"{############################### decodeBuiltinType ###############################}\n"
"{% def decodeBuiltinType(info) --------------- BuiltinType %}\n"
"{% if info.isReference%} \n"
"{$info.name}.set(codec.read{$info.type.codecTypeName}());\n"
"{% else %}\n"
"{% if info.needTypeDeclaration %}{$info.type.typeName} {% endif %}{$info.name} = codec.read{$info.type.codecTypeName}();\n"
"{% endif -- isReference%}\n"
"{% enddef %}\n"
"\n"
"{############################### decodeStructType ###############################}\n"
"{% def decodeStructType(info) --------------- StructType %}\n"
"{% if info.isReference%}\n"
"{$info.name}.set({$info.type.name}.read(codec));\n"
"{% else %}\n"
"{% if info.needTypeDeclaration %}{$info.type.typeName} {% endif %}{$info.name} = {$info.type.name}.read(codec);\n"
"{% endif -- isReference%}\n"
"{% enddef -------------------------- StructType %}\n"
"\n"
"{############################### decodeArrayType ###############################}\n"
"{% def decodeArrayType(info) -------------- ArrayType %}\n"
"{% if info.isReference%} \n"
"{$info.name}.set({$info.initialization});\n"
"{% else %}\n"
"{% if info.needTypeDeclaration %}{$info.type.typeName} {% endif %}{$info.name} = {$info.initialization};\n"
"{% endif -- isReference%}\n"
"for (int {$info.counter} = 0; {$info.counter} < {$info.size}; ++{$info.counter})\n"
"{\n"
"{$ addIndent(\"    \", info.protoNext.decode(info.protoNext))}\n"
"    {$info.name}{% if info.isReference%}.get(){% endif %}[{$info.counter}] = {$info.protoNext.name};\n"
"}\n"
"{% enddef ----------------------------------- ArrayType %}\n"
"\n"
"{############################### decodeUnionType ###############################}\n"
"{% def decodeUnionType(info) ---------------- %}\n"
"{#var _{$name} = {$commonPrefix}{$info.name}().read({$codec}){%>%};\n"
"var {$name} = _{$name}.getFirst();\n"
"var {$type.discriminatorName} = {$info.name}.get(_{$name}.getSecond());#}\n"
"{% enddef -------------------------- UnionType %}\n"
"\n"
"{############################### decodeEnumType ###############################}\n"
"{% def decodeEnumType(info) ---------------- EnumType %}\n"
"{% if info.isReference%}\n"
"{$info.name}.set({$info.type.name}.read(codec));\n"
"{% else %}\n"
"{% if info.needTypeDeclaration %}{$info.type.typeName} {% endif %}{$info.name} = ({$info.type.name})codec.readInt32();\n"
"{% endif -- isReference%}\n"
"{% enddef ---------------------------------- EnumType %}\n"
"\n"
"{############################### decodeBinaryType ###############################}\n"
"{% def decodeBinaryType(info) ------------------- BinaryType %}\n"
"{% if info.needTypeDeclaration %}{$info.type.typeName} {% endif %}{$info.name} = codec.readBinary();{%>%}\n"
"{% enddef ------------------------------------- BinaryType %}\n"
"\n"
"{############################### decodeListType ###############################}\n"
"{% def decodeListType(info) ------------------- ListType %}\n"
"long {$info.sizeVariable} = {% if info.hasLengthVariable %}{$info.size}{% else %}codec.startReadList(){% endif %};\n"
"{% if info.isReference%}\n"
"{$info.name}.set(new ());\n"
"{% else %}\n"
"{% if info.needTypeDeclaration %}{$info.type.name} {% endif %}{$info.name} = new {$info.type.typeName}();\n"
"{% endif -- isReference%}\n"
"for (long {$info.counter} = 0L; {$info.counter} < {$info.sizeVariable}; ++{$info.counter}) {\n"
"{$addIndent(\"    \", info.protoNext.decode(info.protoNext))}\n"
"    {$info.name}{% if info.isReference%}.get(){% endif %}.Add({$info.protoNext.name});\n"
"}\n"
"{% enddef ------------------------------------- ListType %}\n"
"\n"
"\n"
"{##################################################################################}\n"
"{################################### ENCODING #####################################}\n"
"{##################################################################################}\n"
"\n"
"{############################### encodeBuiltinType ###############################}\n"
"{% def encodeBuiltinType(info) --------------- BuiltinType %}\n"
"codec.write{$info.type.codecTypeName}({$info.name}{% if info.isReference%}.get(){% endif %});\n"
"{% enddef %}\n"
"\n"
"{############################### encodeArrayType ###############################}\n"
"{% def encodeArrayType(info) -------------- ArrayType %}\n"
"for (int {$info.counter} = 0; {$info.counter} < {$info.size}; ++{$info.counter})\n"
"{\n"
"    {$info.protoNext.type.typeName} {$info.protoNext.name} = {%if info.isStructMember %}this.{% endif %}{$info.name}{% if info.isReference%}.get(){% endif %}[{$info.counter}];\n"
"{$addIndent(\"    \", info.protoNext.encode(info.protoNext))}\n"
"}\n"
"{% enddef ----------------------------------- ArrayType %}\n"
"\n"
"{############################### encodeStructType ###############################}\n"
"{% def encodeStructType(info) --------------- StructType %}\n"
"{%if info.isStructMember %}this.{% endif %}{$info.name}{% if info.isReference%}.get(){% endif %}.write(codec);\n"
"{% enddef -------------------------- StructType %}\n"
"\n"
"{############################### encodeUnionType ###############################}\n"
"{% def encodeUnionType(info) ---------------- %}\n"
"\n"
"{% enddef -------------------------- UnionType %}\n"
"\n"
"{############################### encodeEnumType ###############################}\n"
"{% def encodeEnumType(info) ---------------- EnumType %}\n"
"codec.writeInt32((int){%if info.isStructMember %}this.{% endif %}{$info.name}{% if info.isReference%}.get(){% endif %});\n"
"{% enddef ---------------------------------- EnumType %}\n"
"\n"
"{############################### encodeBinaryType ###############################}\n"
"{% def encodeBinaryType(info) ------------------- BinaryType %}\n"
"codec.writeBinary({%if info.isStructMember %}this.{% endif %}{$info.name}{% if info.isReference%}.get(){% endif %});{%>%}\n"
"{% enddef ------------------------------------- BinaryType %}\n"
"\n"
"{############################### encodeListType ###############################}\n"
"{% def encodeListType(info) ------------------- ListType %}\n"
"codec.startWriteList({$info.name}{% if info.isReference%}.get(){% endif %}.Count);\n"
"foreach ({$info.protoNext.type.typeName} {$info.protoNext.name} in {%if info.isStructMember %}this.{% endif %}{$info.name}{% if info.isReference%}.get(){% endif %}) {\n"
"{$   addIndent(\"    \", info.protoNext.encode(info.protoNext))}\n"
"}\n"
"{% enddef ------------------------------------- ListType %}\n"
"\n"
"{############################### union encode/decode ###############################}\n"
"{% def encodeValueUnion(info, name, codec, indent, depth) %}\n"
"{%  if info.isNonEncapsulatedUnion %}\n"
"{%   set self = \"\" %}\n"
"{%  else %}\n"
"{%   set self = \"self.\" %}\n"
"{%  endif %}\n"
"codec.start_write_union({$self}{$info.discriminatorName})\n"
"{# unions are always within structs, so we have self available #}\n"
"{%  set isFirst = true %}\n"
"{%  set hasNonVoidCase = false %}\n"
"{%  set defaultCase = false %}\n"
"{%  for c in info.cases %}\n"
"{%   if c.name == \"default\" %}\n"
"{%    set defaultCase = c %}\n"
"{%   elif not c.isVoid %}\n"
"{%    set hasNonVoidCase = true %}\n"
"{$indent}{$\"if\" if isFirst else \"elif\"} {$self}{$info.discriminatorName} == {% if c.name != \"\" && (c.type.type == \"enum\" || c.type.type == \"const\") %}{% if c.type.name != \"\" %}{$c.type.name}.{%  endif %}{$c.name}{% else %}{$c.value}{%  endif %}:\n"
"{%    for cm in c.members %}\n"
"{%     if cm.isNullable %}\n"
"{$indent}    if {$name}.{$cm.name} is None:\n"
"{$indent}        {$codec}.write_null_flag(True)\n"
"{$indent}    else:\n"
"{$indent}        {$codec}.write_null_flag(False)\n"
"{$indent}    {$encodeValue(cm.type, name & \".\" & cm.name, codec, indent & \"    \", depth + 1)}\n"
"{%     else -- isNullable %}\n"
"{$indent}    if {$name}.{$cm.name} is None:\n"
"{$indent}        raise ValueError(\"{$name}.{$cm.name} is None\")\n"
"{$indent}    {$encodeValue(cm.type, name & \".\" & cm.name, codec, indent & \"    \", depth + 1)}\n"
"{%     endif -- isNullable %}\n"
"{%    endfor -- union case members %}\n"
"{%    set isFirst = false %}\n"
"{%   endif -- default case/not void %}\n"
"{%  endfor -- union cases %}\n"
"{%  if defaultCase %}\n"
"{%   if not isFirst %}\n"
"{$indent}else: # default case\n"
"{%   endif %}\n"
"{%   if defaultCase.isVoid && not isFirst %}\n"
"{$indent}    pass\n"
"{%   else %}\n"
"{%    for cm in defaultCase.members %}\n"
"{%     if cm.isNullable %}\n"
"{$indent}    if {$name}.{$cm.name} is None:\n"
"{$indent}        {$codec}.write_null_flag(True)\n"
"{$indent}    else:\n"
"{$indent}        {$codec}.write_null_flag(False)\n"
"{$indent}    {% if not isFirst %}    {% endif %}{$encodeValue(cm.type, name & \".\" & cm.name, codec, indent & \"    \", depth + 1)}\n"
"{%     else -- isNullable %}\n"
"{$indent}{% if not isFirst %}    {% endif %}{$encodeValue(cm.type, name & \".\" & cm.name, codec, indent & \"    \", depth + 1)}\n"
"{%     endif -- isNullable %}\n"
"{%    endfor -- union case members %}\n"
"{%   endif -- defaultCase.isVoid %}\n"
"{%  elif hasNonVoidCase %}\n"
"{$indent}else:\n"
"{$indent}    raise ValueError(\"invalid union discriminator value %s\" % repr({$self}{$info.discriminatorName}))\n"
"{%  endif -- defaultCase/hasNonVoidCase %}\n"
"{% enddef %}\n"
"\n"
"{% def decodeValueUnion(info, name, codec, indent, depth) %}\n"
"{%  if info.isNonEncapsulatedUnion %}\n"
"{%   set self = \"\" %}\n"
"{%  else %}\n"
"{%   set self = \"self.\" %}\n"
"{%  endif %}\n"
"{$self}{$info.discriminatorName} = codec.start_read_union()\n"
"{%  if self == \"self.\" %}\n"
"{$indent}{$name} = {$name}_union()\n"
"{%  endif %}\n"
"{%  set isFirst = true %}\n"
"{%  set hasNonVoidCase = false %}\n"
"{%  set defaultCase = false %}\n"
"{%  for c in info.cases %}\n"
"{%   if c.name == \"default\" %}\n"
"{%    set defaultCase = c %}\n"
"{%   elif not c.isVoid %}\n"
"{%    set hasNonVoidCase = true %}\n"
"{$indent}{$\"if\" if isFirst else \"elif\"} {$self}{$info.discriminatorName} == {% if c.name != \"\" && (c.type.type == \"enum\" || c.type.type == \"const\") %}{% if c.type.name != \"\" %}{$c.type.name}.{%  endif %}{$c.name}{% else %}{$c.value}{%  endif %}:\n"
"{%    for cm in c.members %}\n"
"{%     if cm.isNullable %}\n"
"{$indent}    if not {$codec}.read_null_flag()\n"
"{$indent}        {$decodeValue(cm.type, name & \".\" & cm.name, codec, indent & \"    \", depth + 1)}\n"
"{$indent}    else:\n"
"{$indent}        {$name}.{$cm.name} = None\n"
"{%     else -- isNullable %}\n"
"{$indent}    {$decodeValue(cm.type, name & \".\" & cm.name, codec, indent & \"    \", depth + 1)}\n"
"{%     endif -- isNullable %}\n"
"{%    endfor -- union case members %}\n"
"{%    set isFirst = false %}\n"
"{%   endif -- default/not void %}\n"
"{%  endfor -- union cases %}\n"
"{%  if defaultCase %}\n"
"{%   if not isFirst %}\n"
"{$indent}else: # default case\n"
"{%   endif %}\n"
"{%   if defaultCase.isVoid && not isFirst %}\n"
"{$indent}    pass\n"
"{%   else %}\n"
"{%    for cm in defaultCase.members %}\n"
"{%     if cm.isNullable %}\n"
"{$indent}    if not {$codec}.read_null_flag()\n"
"{$indent}    {% if not isFirst %}    {% endif %}{$decodeValue(cm.type, name & \".\" & cm.name, codec, indent & \"    \", depth + 1)}\n"
"{$indent}    else:\n"
"{$indent}        {$name}.{$cm.name} = None\n"
"{%     else -- isNullable %}\n"
"{$indent}{% if not isFirst %}    {% endif %}{$decodeValue(cm.type, name & \".\" & cm.name, codec, indent & \"    \", depth + 1)}\n"
"{%     endif -- isNullable %}\n"
"{%    endfor -- union case members %}\n"
"{%   endif -- defaultCase.isVoid %}\n"
"{%  elif hasNonVoidCase %}\n"
"{$indent}else:\n"
"{$indent}    raise ValueError(\"invalid union discriminator value %s\" % repr({$self}{$info.discriminatorName}))\n"
"{%  endif -- default/hasNonVoidCase %}\n"
"{% enddef %}\n"
"\n"
"{% def getTypeValuePosfix(type) %}\n"
"{% if type.type == \"float\" %}F{% elif type.type == \"long\" %}L{% elif type.type == \"double\" %}D{% endif %}{%>%}\n"
"{% enddef%}"
;

