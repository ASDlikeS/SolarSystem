CREATE TABLE planets
(
    id               serial PRIMARY KEY,
    name             varchar(30) NOT NULL,
    mass_kg          float8 NOT NULL,
    radius_m         float8 NOT NULL,
    orbit_radius_m   float8 NOT NULL,
    orbital_speed_ms float8 NOT NULL,
    color_hex        varchar(7) DEFAULT '#FFFFFF' NOT NULL,
    parent_id        int REFERENCES planets(id),
    texture_path     varchar(255)
);

CREATE INDEX idx_planets_parent ON planets(parent_id);

INSERT INTO planets (name, mass_kg, radius_m, orbit_radius_m, orbital_speed_ms, color_hex, parent_id, texture_path) VALUES
('Mercury', 3.301e23, 2439700, 57910000000,   47400, '#A9A9A9', NULL, 'mercury.png'),
('Venus',   4.867e24, 6051800, 108200000000,  35000, '#E8C07D', NULL, 'venus.png'),
('Earth',   5.972e24, 6371000, 149600000000,  29800, '#3B82F6', NULL, 'earth.png'),
('Mars',    6.417e23, 3389500, 227900000000,  24100, '#C1440E', NULL, 'mars.png'),
('Jupiter', 1.898e27, 69911000, 778500000000, 13100, '#D8A47F', NULL, 'jupiter.png'),
('Saturn',  5.683e26, 58232000, 1432000000000, 9680, '#E3D9A6', NULL, 'saturn.png'),
('Uranus',  8.681e25, 25362000, 2867000000000, 6800, '#7DE3E8', NULL, 'uranus.png'),
('Neptune', 1.024e26, 24622000, 4495000000000, 5430, '#3E5EF7', NULL, 'neptune.png');

INSERT INTO planets (name, mass_kg, radius_m, orbit_radius_m, orbital_speed_ms, color_hex, parent_id, texture_path) 
VALUES ('Moon', 7.342e22, 1737400, 384400000, 1022, '#CCCCCC', 3, 'moon.png');