precision highp float;
uniform vec4 u_color;
uniform vec3 u_light_dir;
uniform vec4 u_light_color;
uniform float u_ambient;
uniform vec3 u_eye;

uniform sampler2D u_color_texture;
uniform sampler2D u_normal_texture;
uniform sampler2D u_specular_texture;

varying vec3 v_normal;
varying vec3 v_pos;
varying vec2 v_coord;


void main() 
{


        normalize(v_normal);
        vec4 tex_color = texture2D(u_color_texture, v_coord);
        vec4 normal = texture2D(u_normal_texture, v_coord );
        vec4 specular = texture2D(u_specular_texture, v_coord );

        vec3 L = normalize(u_light_dir);
        float D =  max(0.0, dot( L , v_normal)); // diffuse component
        vec3 point_to_eye = normalize( u_eye - v_pos);
        vec3 H = reflect(-L,v_normal); // vector reflected in the incident point
        float E = pow(max(0.0, dot( H , point_to_eye)),8.0); // the closer the dot is to 1 the shiner
        gl_FragColor = u_color * tex_color *  (u_ambient + D) + u_light_color * E * specular.x;


}