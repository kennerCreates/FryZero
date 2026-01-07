shader_type canvas_item;

uniform vec3 Light : source_color;
uniform vec3 LightHighlight : source_color;
uniform vec3 LightShadow : source_color;
uniform vec3 LightAccent : source_color;

uniform vec3 Dark : source_color;
uniform vec3 DarkHighlight : source_color;
uniform vec3 DarkShadow : source_color;
uniform vec3 DarkAccent : source_color;

const vec3 light_marker = vec3(1.0, 1.0, 1.0); //white
const vec3 light_highlight_marker = vec3(1.0, 1.0, 0.0); //yellow
const vec3 light_shadow_marker = vec3(1.0, 0.0, 1.0); //magenta
const vec3 light_accent_marker = vec3(1.0, 0.0, 0.0); //red

const vec3 dark_marker = vec3(0.0, 0.0, 0.0); //black
const vec3 dark_highlight_marker = vec3(0.0, 1.0, 1.0); //cyan
const vec3 dark_shadow_marker = vec3(0.0, 0.0, 1.0); //blue
const vec3 dark_accent_marker = vec3(0.0, 1.0, 0.0); //green


void fragment(){
	vec4 tex = texture(TEXTURE,UV);

	vec3 tex_rgb = tex.rgb;

	if (tex_rgb == light_marker){
		COLOR.rgb = Light;
	}
	else if (tex_rgb == light_highlight_marker){
		COLOR.rgb = LightHighlight;
	}
	else if (tex_rgb == light_shadow_marker){
		COLOR.rgb = LightShadow;
	}
	else if (tex_rgb == light_accent_marker){
		COLOR.rgb = LightAccent;
	}
	else if (tex_rgb == dark_marker){
		COLOR.rgb = Dark;
	}
	else if (tex_rgb == dark_highlight_marker){
		COLOR.rgb = DarkHighlight;
	}
	else if (tex_rgb == dark_shadow_marker){
		COLOR.rgb = DarkShadow;
	}
	else if (tex_rgb == dark_accent_marker){
		COLOR.rgb = DarkAccent;
	}
	else{
		COLOR.rgb = Light;
	}
}
