shader_type canvas_item;

uniform vec3 light_color : source_color;
uniform vec3 light_highlight_color : source_color;
uniform vec3 light_shadow_color : source_color;
uniform vec3 light_accent_color : source_color;

uniform vec3 dark_color : source_color;
uniform vec3 dark_highlight_color : source_color;
uniform vec3 dark_shadow_color : source_color;
uniform vec3 dark_accent_color : source_color;

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
		COLOR.rgb = light_color;
	}
	else if (tex_rgb == light_highlight_marker){
		COLOR.rgb = light_highlight_color;
	}
	else if (tex_rgb == light_shadow_marker){
		COLOR.rgb = light_shadow_color;
	}
	else if (tex_rgb == light_highlight_marker){
		COLOR.rgb = light_highlight_color;
	}
	else if (tex_rgb == dark_marker){
		COLOR.rgb = dark_color;
	}
	else if (tex_rgb == dark_highlight_marker){
		COLOR.rgb = dark_highlight_color;
	}
	else if (tex_rgb == dark_shadow_marker){
		COLOR.rgb = dark_shadow_color;
	}
	else if (tex_rgb == dark_highlight_marker){
		COLOR.rgb = dark_highlight_color;
	}
	else{
		COLOR.rgb = light_color;
	}
}
