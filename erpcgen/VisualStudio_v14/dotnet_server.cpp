/*
 * AUTOMATICALLY GENERATED FILE - DO NOT EDIT
 */

// Generated from ..\src\templates\dotnet_server.template
extern const char * const kDotnetServer;

const char * const kDotnetServer = 
"/** \n"
" * Generated by erpcgen {$erpcVersion} on {$todaysDate}.\n"
" * \n"
" * AUTOGENERATED - DO NOT EDIT\n"
" */\n"
"{% set iface = interface %}\n"
"namespace {$groupPackage}.server;\n"
"\n"
"using {$groupPackage}.interfaces.I{$iface.name};\n"
"{% if not empty(group.symbolsMap.structs) %}using {$groupPackage}.common.structs;{% elif not empty(structs)%}using {$groupPackage}.common.structs;{% endif %}\n"
"{% if not empty(enums)%}using {$groupPackage}.common.enums;{% endif %}\n"
"\n"
"using io.github.embeddedrpc.erpc.auxiliary.MessageInfo;\n"
"using io.github.embeddedrpc.erpc.auxiliary.MessageType;\n"
"using io.github.embeddedrpc.erpc.codec.Codec;\n"
"using io.github.embeddedrpc.erpc.server.Service;\n"
"using io.github.embeddedrpc.erpc.auxiliary.Reference;\n"
"\n"
"{%  for inc in includes %}\n"
"using {$inc};\n"
"{%  endfor -- includes %}\n"
"\n"
"using System.Collections.Generic;\n"
"\n"
"/**\n"
" * Testing abstract service class for simple eRPC interface.\n"
" */\n"
"public abstract class Abstract{$iface.name}Service\n"
"        : Service, I{$iface.name} {\n"
"\n"
"    /**\n"
"     * Default constructor.\n"
"     */\n"
"    public Abstract{$iface.name}Service() {\n"
"        base(I{$iface.name}.SERVICE_ID);\n"
"{% for fn in iface.functions %}\n"
"        addMethodHandler(I{$iface.name}.{$upper(fn.name)}_ID, this.{$fn.name}Handler);\n"
"{% endfor -- fn %}\n"
"    }\n"
"\n"
"{% for fn in iface.functions %}\n"
"    private void {$fn.name}Handler(int sequence, Codec codec) {\n"
"{%          for p in fn.parameters if not p.serializedViaMember %}\n"
"        {$p.call.type.typeName} {$p.name}{% if p.call.isReference %} = new Reference<>(){% endif%};\n"
"{%          endfor %}\n"
"\n"
"{%          for p in fn.inParameters if not p.serializedViaMember %}\n"
"{% set tmp = p.call.needTypeDeclaration%}\n"
"{% set p.call.needTypeDeclaration = false %}\n"
"{$ addIndent(\"        \", p.call.decode(p.call))}\n"
"{% set p.call.needTypeDeclaration = tmp %}\n"
"{%          endfor %}\n"
"\n"
"        {%  if not fn.isOneway %}{% if fn.isReturnValue && fn.returnValue.type.type != \"void\" %}{$fn.returnValue.call.type.typeName} _result = {% endif %}{% endif %}{$fn.name}({% for p in fn.parameters if not p.serializedViaMember %}{$p.name}{% if !loop.last%}, {% endif %}{% endfor %});\n"
"\n"
"        codec.reset();\n"
"\n"
"{%  if not fn.isOneway %}\n"
"        codec.startWriteMessage(new MessageInfo(\n"
"                MessageType.kReplyMessage,\n"
"                getServiceId(),\n"
"                I{$iface.name}.{$upper(fn.name)}_ID,\n"
"                sequence)\n"
"        );\n"
"\n"
"        // Read out parameters\n"
"{% for p in fn.outParameters if not p.serializedViaMember %}\n"
"{%     if p.isNullable %}\n"
"        if(!codec.readNullFlag()) {\n"
"{$         addIndent(\"            \", p.call.encode(p.call))}\n"
"        }\n"
"{%     else %}\n"
"{$         addIndent(\"        \", p.call.encode(p.call))}\n"
"{%     endif -- isNullable %}\n"
"{% endfor -- outParams %}\n"
"{%      if fn.isReturnValue && fn.returnValue.type.type != \"void\" %}\n"
"        // Read return value\n"
"{$      addIndent(\"        \",  fn.returnValue.call.encode(fn.returnValue.call))}\n"
"{%      endif -- returnValue %}\n"
"{%  endif -- oneway %}\n"
"    }\n"
"{% endfor -- fn %}\n"
"\n"
"}\n"
"\n"
;

