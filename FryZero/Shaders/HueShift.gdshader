shader_type canvas_item;

uniform vec3 light_color : source_color = vec3(1.0, 1.0, 1.0); //white;
uniform vec3 light_highlight_color : source_color = vec3(1.0, 1.0, 0.0); //yellow
uniform vec3 light_shadow_color : source_color = vec3(1.0, 0.0, 1.0); //magenta
uniform vec3 light_accent_color : source_color = vec3(1.0, 0.0, 0.0); //red

uniform vec3 dark_color : source_color = vec3(0.0, 0.0, 0.0); //black
uniform vec3 dark_highlight_color : source_color = vec3(0.0, 1.0, 1.0); //cyan
uniform vec3 dark_shadow_color : source_color = vec3(0.0, 0.0, 1.0); //blue
uniform vec3 dark_accent_color : source_color = vec3(0.0, 1.0, 0.0); //green

const vec3 light_marker = vec3(1.0, 1.0, 1.0); //white
const vec3 light_highlight_marker = vec3(1.0, 1.0, 0.0); //yellow
const vec3 light_shadow_marker = vec3(1.0, 0.0, 1.0); //magenta
const vec3 light_accent_marker = vec3(1.0, 0.0, 0.0); //red

const vec3 dark_marker = vec3(0.0, 0.0, 0.0); //black
const vec3 dark_highlight_marker = vec3(0.0, 1.0, 1.0); //cyan
const vec3 dark_shadow_marker = vec3(0.0, 0.0, 1.0); //blue
const vec3 dark_accent_marker = vec3(0.0, 1.0, 0.0); //green

const float precision_threshold = 0.1;

void fragment(){
	vec4 tex = texture(TEXTURE,UV);

	vec3 marker = tex.rgb * COLOR.rgb;

	vec3 result_color = light_color;

	if (distance(marker, light_marker) < precision_threshold){
		result_color = light_color;
	}
	else if (distance(marker, light_highlight_marker) < precision_threshold){
		result_color = light_highlight_color;
	}
	else if (distance(marker, light_shadow_marker) < precision_threshold){
		result_color = light_shadow_color;
	}
	else if (distance(marker, light_accent_marker) < precision_threshold){
		result_color = light_accent_color;
	}
	else if (distance(marker, dark_marker) < precision_threshold){
		result_color = dark_color;
	}
	else if (distance(marker, dark_highlight_marker) < precision_threshold){
		result_color = dark_highlight_color;
	}
	else if (distance(marker, dark_shadow_marker) < precision_threshold){
		result_color = dark_shadow_color;
	}
	else if (distance(marker, dark_accent_marker) < precision_threshold){
		result_color = dark_accent_color;
	}
	else{
		result_color = light_color;
	}

	COLOR.rgb = result_color;
	COLOR.a = tex.a;
}