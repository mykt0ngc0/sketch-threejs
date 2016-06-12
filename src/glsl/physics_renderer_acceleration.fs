uniform vec2 resolution;
uniform sampler2D velocity;
uniform sampler2D acceleration;
uniform vec2 anchor;

varying vec2 vUv;

#define PRECISION 0.000001

vec3 drag(vec3 a, float value) {
  return normalize(a * -1.0 + PRECISION) * length(a) * value;
}

vec3 hook(vec3 v, vec3 anchor, float rest_length, float k) {
  return normalize(v - anchor + PRECISION) * (-1.0 * k * (length(v - anchor) - rest_length));
}

vec3 attract(vec3 v1, vec3 v2, float m1, float m2, float g) {
  return g * m1 * m2 / pow(clamp(length(v2 - v1), 5.0, 30.0), 2.0) * normalize(v2 - v1 + PRECISION);
}

void main(void) {
  vec3 v = texture2D(velocity, vUv).xyz;
  vec3 a = texture2D(acceleration, vUv).xyz;
  vec3 a2 = a + attract(v, vec3(anchor.x * 80.0, anchor.y * 80.0, 0.0), 1.0, 1.0, 10.0);
  gl_FragColor = vec4(a2, 1.0);
}