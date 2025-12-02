shader_type canvas_item;

uniform vec3 main_color : source_color;
uniform vec3 outline_color : source_color;

const vec3 shadow_marker = vec3(0.0, 1.0, 1.0); //cyan
const vec3 highlight_marker = vec3(1.0, 1.0, 0.0); //yellow
const vec3 outline_marker = vec3(0.0, 0.0, 0.0);

void fragment(){
	vec4 tex = texture(TEXTURE,UV);
	vec3 tex_rgb = tex.rgb;
	
	if (tex_rgb == shadow_marker){
		COLOR.rgb = main_color * vec3(0.65, 0.65, 0.9);
	}
	else if (tex_rgb == highlight_marker){
		COLOR.rgb = main_color * vec3(1.4, 1.4, 0.9);
	}
	else if (tex_rgb == outline_marker){
		COLOR.rgb = outline_color;
	}
	else{
		COLOR.rgb = main_color;
	}
}


