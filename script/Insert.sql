INSERT INTO TIPOUSUARIOS (Tipo) VALUES ('ADMINISTRADOR'),('CLIENTE');

INSERT INTO CATEGORIAS (Nombre, Estado) VALUES ('Arduino', 1);
INSERT INTO CATEGORIAS (Nombre, Estado) VALUES ('Sensores', 1);
INSERT INTO CATEGORIAS (Nombre, Estado) VALUES ('Módulos', 1);
INSERT INTO CATEGORIAS (Nombre, Estado) VALUES ('Componentes', 1);
INSERT INTO CATEGORIAS (Nombre, Estado) VALUES ('Herramientas', 1);
INSERT INTO CATEGORIAS (Nombre, Estado) VALUES ('Componentes Electrónicos', 1);
INSERT INTO CATEGORIAS (Nombre, Estado) VALUES ('Energía y Baterías', 1);
INSERT INTO CATEGORIAS (Nombre, Estado) VALUES ('Iluminación LED', 1);
INSERT INTO CATEGORIAS (Nombre, Estado) VALUES ('Microcontroladores', 1);
INSERT INTO CATEGORIAS (Nombre, Estado) VALUES ('Impresión 3D', 1);
INSERT INTO CATEGORIAS (Nombre, Estado) VALUES ('Audio y Sonido', 1);
INSERT INTO CATEGORIAS (Nombre, Estado) VALUES ('Cables y Conectores', 1);
INSERT INTO CATEGORIAS (Nombre, Estado) VALUES ('Módulos de Comunicación', 1);


INSERT INTO MARCAS (Nombre, Estado) VALUES ('Genérica', 1);
INSERT INTO MARCAS (Nombre, Estado) VALUES ('Elegoo', 1);
INSERT INTO MARCAS (Nombre, Estado) VALUES ('Keystudio', 1);
INSERT INTO MARCAS (Nombre, Estado) VALUES ('DFRobot', 1);
INSERT INTO MARCAS (Nombre, Estado) VALUES ('apple', 1);
INSERT INTO MARCAS (Nombre, Estado) VALUES ('SanDisk', 1);
INSERT INTO MARCAS (Nombre, Estado) VALUES ('Kingston', 1);
INSERT INTO MARCAS (Nombre, Estado) VALUES ('Mean Well', 1);
INSERT INTO MARCAS (Nombre, Estado) VALUES ('Anker', 1);
INSERT INTO MARCAS (Nombre, Estado) VALUES ('Arduino', 1);
INSERT INTO MARCAS (Nombre, Estado) VALUES ('Makita', 1);
INSERT INTO MARCAS (Nombre, Estado) VALUES ('Bosch', 1);
INSERT INTO MARCAS (Nombre, Estado) VALUES ('Philips', 1);


INSERT INTO ARTICULOS
    (Codigo, Cantidad, Precio, IdCategoria, IdMarca, Nombre, Descripcion, Estado)
VALUES
('UYT', 5, 14000.0000, 3, 10, 'ARDUINO NANO V3.0 ATMEGA328P ARMADO',
 'El Arduino Nano es una placa pequeña, completa y fácil de usar basado en el ATmega328P (Arduino Nano). Esta puede alimentarse a través de la conexión USB Mini-B, una fuente de alimentación externa no regulada de 6-12V o una fuente de alimentación externa regulada de 5V. La fuente de poder se selecciona automáticamente a la fuente de voltaje más alta. El ATmega328P tiene 32 KB, (también se usan 2 KB para el gestor de arranque). El ATmega328P tiene 2 KB de SRAM y 1 KB de EEPROM.', 1),

('QSS', 6, 11000.0000, 3, 10, 'TB6612FNG DRIVER MOTOR',
 'El TB6612FNG es un controlador de motores que permite manejar hasta dos motores DC o un motor paso a paso mediante dos puentes H integrados. Cada canal soporta corrientes continuas de hasta 1.2 A (con picos de 3.2 A), ofreciendo modos de operación como giro horario (CW), antihorario (CCW), freno corto y parada. Gracias a su diseño con transistores MOSFET, es más eficiente y compacto que controladores tradicionales como el L298N. Es ampliamente utilizado en proyectos de robótica y electrónica junto a plataformas como Arduino o Raspberry Pi, proporcionando control preciso y confiable.', 1),

('QAA', 7, 4000.0000, 3, 10, 'NODEMCU ESP32 WIFI + BLUETOOTH',
 'El NodeMCU ESP32 es un microcontrolador avanzado basado en el chip ESP-WROOM-32, que integra un procesador Tensilica Xtensa LX6 de doble núcleo capaz de alcanzar hasta 240 MHz. Dispone de 520 kB de RAM, memoria Flash de 4 MB y conectividad WiFi y Bluetooth 4.2 BLE, lo que lo convierte en una plataforma ideal para proyectos de IoT, domótica y servidores conectados. Su bajo consumo, gran capacidad de cálculo y facilidad de programación lo hacen superior a placas como Arduino en aplicaciones que requieren comunicación inalámbrica y procesamiento intensivo.', 1),

('AAA', 5, 520000.0000, 10, 8, 'IMPRESORA 3D ARTILLERY HORNET',
 'La nueva impresora 3D Artillery Hornet, es una impresora 3D de estilo cartesiano todo en uno. La impresora Artillery Hornet es ultra silenciosa y tiene controladores 32bits paso a paso, tiene un extrusor tipo bowden, nozzle 0.4mm compatible E3D V6, cama calefactada de corriente CA.', 1),

('BBB', 9, 400000.0000, 10, 8, 'IMPRESORA 3D CREALITY ENDER-3 V2',
 'La Ender-3 V2 de Creality mejora las clásicas y conocidas Ender-3 y Ender-3 Pro. Continúa fiel tanto a usuarios principiantes como avanzados, siendo una de las favoritas en el mercado. Hereda la estructura Full Metal, resistente y compacta pero actualizada con una serie de mejoras. Ofrece un buen volumen de impresión, es fácil de montar e imprime en alta calidad.', 1),

('ASD', 0, 8400.0000, 12, 1, 'Cable Hdmi A Hdmi Reforzado Mallado - 1.5 Mts',
 'Con entrada HDMI y salida HDMI.', 1),

('DSA', 0, 7800.0000, 12, 1, 'Cable Vga 3 Metros Con Filtro',
 'Con entrada VGA y salida VGA.', 1),

('YYY', 10, 60000.0000, 11, 13, 'PARLANTE BLUETOOTH JBL GO ESSENTIAL AZUL',
 'Descubrí la calidad de sonido excepcional y la portabilidad que necesitás con el Parlante Bluetooth JBL GO Essential Azul. Este parlante compacto es ideal para quienes buscan disfrutar de su música favorita en cualquier lugar, sin perder definición ni potencia.', 1),

('QWQ', 10, 80000.0000, 11, 13, 'ALEXA ECHO POP C2H4R9 AMAZON BLANCO',
 'Descubrí la Alexa Echo Pop C2H4R9 de Amazon en color blanco, el dispositivo inteligente que revolucionará tu hogar con solo tu voz. Compacta y elegante, esta bocina inteligente combina un diseño moderno con la potencia de Alexa para ofrecerte respuestas rápidas, control de dispositivos compatibles, y entretenimiento sin límites.', 1),

('RRS', 10, 205000.0000, 11, 13, 'XIAOMI ASPIRADORA ROBOT VACUUM E5',
 'La XIAOMI ASPIRADORA ROBOT VACUUM E5 es la solución inteligente para mantener tu hogar impecable sin esfuerzo. Diseñada con tecnología avanzada, esta aspiradora robot combina potencia y precisión para limpiar cada rincón de tu casa de manera eficiente y silenciosa. Con su sistema de navegación inteligente y sensores de alta sensibilidad, el Vacuum E5 detecta obstáculos y adapta su ruta para maximizar la limpieza. Su potente motor garantiza una succión profunda, ideal para todo tipo de superficies, desde pisos de madera hasta alfombras.', 1),

('ZAS', 25, 9200.0000, 5, 10, 'Puntas de tester CAT III 1000V 20A',
 'Puntas de tester CAT III 1000V 20A son la herramienta ideal para profesionales que requieren mediciones seguras y precisas en instalaciones eléctricas de alta demanda. Con certificación CAT III, estas puntas garantizan protección hasta 1000V y una capacidad de corriente máxima de 20A, asegurando resistencia y fiabilidad en ambientes industriales.', 1),

('RTO', 10, 3200.0000, 5, 10, 'Desoldador A Piston Chupador De Estaño Metálico',
 'El desoldador a pistón metálico es una herramienta esencial para trabajos de reparación, mantenimiento y montaje electrónico. Permite remover estaño de forma rápida y precisa, dejando las pistas y componentes listos para volver a soldar. Su cuerpo metálico ofrece mayor durabilidad y resistencia al calor.', 1),

('UUT', 8, 16360.0000, 5, 10, 'SOLDADOR TIPO LAPIZ 40W PUNTA CERAMICA',
 'El soldador eléctrico lápiz punta cerámica 40w es la herramienta perfecta para tus proyectos de soldadura. Con su potencia de 40W, podrás realizar trabajos precisos y eficientes. Su diseño compacto y ergonómico te permitirá manejarlo con comodidad y precisión. Ideal para uso profesional o doméstico, este soldador eléctrico te brindará resultados impecables en cada soldadura. No pierdas la oportunidad de adquirir este producto de alta calidad y rendimiento.', 1);




 INSERT INTO IMAGENES (IdArticulo, UrlImagen)
VALUES
(1, '//acdn-us.mitiendanube.com/stores/002/936/119/products/arduino-nano-v30-ch340-atmega328p-s-cable-itytarg-d381cbaf2eb909458517501652970799-1024-1024.webp'),
(2, '//acdn-us.mitiendanube.com/stores/002/936/119/products/2_20240210_114905_0001-2a0d272f8cec46a7a617075767779650-1024-1024.webp'),
(2, '//acdn-us.mitiendanube.com/stores/002/936/119/products/3_20240210_114906_0002-025d24978fb56b8ca517075767781690-1024-1024.webp'),
(2, '//acdn-us.mitiendanube.com/stores/002/936/119/products/4_20240210_114906_0003-b98da589cba283611b17075767784110-1024-1024.webp'),
(2, '//acdn-us.mitiendanube.com/stores/002/936/119/products/5_20240210_114906_0004-4b3e49c6e3ece6471f17075767775295-1024-1024.webp'),
(2, '//acdn-us.mitiendanube.com/stores/002/936/119/products/1_20240210_114905_0000-2c44b24adcf0dec3b017075767784872-1024-1024.webp'),
(3, '//acdn-us.mitiendanube.com/stores/002/936/119/products/placa-desarrollo-esp32-38pin-cp2102-dual-core-wifi-bluetooth-f1-7017d494849a9f274f17629825835709-1024-1024.webp'),
(3, '//acdn-us.mitiendanube.com/stores/002/936/119/products/placa-desarrollo-esp32-38pin-cp2102-dual-core-wifi-bluetooth-f0-0b72055b6cb50e747c17629825837058-1024-1024.webp'),
(4, '//acdn-us.mitiendanube.com/stores/002/936/119/products/diseno-sin-titulo-241-ac0e08ef49439832d716844584548542-1024-1024.webp'),
(4, '//acdn-us.mitiendanube.com/stores/002/936/119/products/diseno-sin-titulo-221-a51eb765e67b4d783c16844584549944-1024-1024.webp'),
(4, '//acdn-us.mitiendanube.com/stores/002/936/119/products/diseno-sin-titulo-231-befccca763534bb37d16844584550366-1024-1024.webp'),
(5, '//acdn-us.mitiendanube.com/stores/002/936/119/products/diseno-sin-titulo-201-3d6cec05209b5740a716844574853089-1024-1024.webp'),
(5, '//acdn-us.mitiendanube.com/stores/002/936/119/products/diseno-sin-titulo-211-3c0afec33b56b4996b16844574852976-1024-1024.webp'),
(5, '//acdn-us.mitiendanube.com/stores/002/936/119/products/diseno-sin-titulo-191-ab4b631e779e85310516844574854595-1024-1024.webp'),
(5, '//acdn-us.mitiendanube.com/stores/002/936/119/products/diseno-sin-titulo-181-3f1de6334ed6413dc516844574853527-1024-1024.webp'),
(5, '//acdn-us.mitiendanube.com/stores/002/936/119/products/diseno-sin-titulo-171-f3ab201491aba5baec16844574855320-1024-1024.webp'),
(6, '//acdn-us.mitiendanube.com/stores/002/936/119/products/d_nq_np_816041-mlu69170408799_042023-o-b6c840a9eee1587ae017540411052007-1024-1024.webp'),
(6, '//acdn-us.mitiendanube.com/stores/002/936/119/products/d_nq_np_769708-mlu69170408803_042023-o-0d481fabbc0f0fa92517540411090615-1024-1024.webp'),
(6, '//acdn-us.mitiendanube.com/stores/002/936/119/products/d_nq_np_998644-mlu69169944697_042023-o-0f89368a9b12c69d9e17540411220736-1024-1024.webp'),
(7, '//acdn-us.mitiendanube.com/stores/002/936/119/products/d_nq_np_893952-mla80586462625_112024-o-7e584522015645166f17540404822674-1024-1024.webp'),
(8, '//acdn-us.mitiendanube.com/stores/002/936/119/products/whatsapp-image-2025-08-21-at-11-12-36-am-8440b2aa71756e8fa317557856015500-1024-1024.webp'),
(9, '//acdn-us.mitiendanube.com/stores/002/936/119/products/whatsapp-image-2025-08-21-at-12-34-36-pm-0dc88b084737d8a71817557904892105-1024-1024.webp'),
(10, '//acdn-us.mitiendanube.com/stores/002/936/119/products/926760_1-c5d9d945a2a4521d8317557908426332-1024-1024.webp'),
(10, '//acdn-us.mitiendanube.com/stores/002/936/119/products/d7cad002d976c17d6a0df13c19d3de2f-096291c9c89b3e4d2817557908423421-1024-1024.webp'),
(11, '//acdn-us.mitiendanube.com/stores/002/936/119/products/images-a0762eb43ec2598b8d17580490848336-1024-1024.webp'),
(12, '//acdn-us.mitiendanube.com/stores/002/936/119/products/d_nq_np_2x_779946-mla31075273560_062019-f-facb21feb6471d802a17628905220521-1024-1024.webp'),
(13, '//acdn-us.mitiendanube.com/stores/002/936/119/products/11-848481e6ae711a2f3917171699376659-1024-1024.webp');


