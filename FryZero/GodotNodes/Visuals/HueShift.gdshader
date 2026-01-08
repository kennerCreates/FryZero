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

	vec3 marker = COLOR.rgb;
	
	vec3 result_color;

	if (marker == light_marker){
		result_color = light_color;
	}
	else if (marker == light_highlight_marker){
		result_color = light_highlight_color;
	}
	else if (marker == light_shadow_marker){
		result_color = light_shadow_color;
	}
	else if (marker == light_accent_marker){
		result_color = light_accent_color;
	}
	else if (marker == dark_marker){
		result_color = dark_color;
	}
	else if (marker == dark_highlight_marker){
		result_color = dark_highlight_color;
	}
	else if (marker == dark_shadow_marker){
		result_color = dark_shadow_color;
	}
	else if (marker == dark_accent_marker){
		result_color = dark_accent_color;
	}
	else{
		result_color = light_color;
	}
	
	COLOR.rgb = result_color;
	COLOR.a = tex.a;
}
