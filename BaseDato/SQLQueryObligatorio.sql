use master 

DROP DATABASE Obligatorio1P3

create database Obligatorio1P3

USE Obligatorio1P3

--#############   PLANTA Y SALTELITES ################

CREATE TABLE TIPO_PLANTA(
        ID_TIPO INT IDENTITY(1, 1) not null,
        NOMBRE VARCHAR(30) NOT NULL UNIQUE,
        DESCRIPCION VARCHAR(200) NOT NULL
        CONSTRAINT PK_ID_TIPO_PLANTA PRIMARY KEY(ID_TIPO)
);
GO

CREATE TABLE PLANTA(
            ID_PLANTA INT IDENTITY(1, 1) NOT NULL,
            NOMBRE_VULGAR VARCHAR(300) NOT NULL,
            NOMBRE_CIENTIFICO VARCHAR(100) NOT NULL,
            DESCRIPCION VARCHAR(500) NOT NULL,
            ALTURA_MAX INT,
            AMBIENTE VARCHAR(30) NOT NULL,
            CONTINENTEORIGEN VARCHAR(30) NOT NULL,
            FOTO VARCHAR(200) NOT NULL,
            ID_TIPO INT NOT NULL,--fk de tipo
            CONSTRAINT PK_ID PRIMARY KEY(ID_PLANTA),--CLAVE PLANATA
            CONSTRAINT FK_ID_TIPO FOREIGN KEY(ID_TIPO) REFERENCES TIPO_PLANTA(ID_TIPO),
);
GO

CREATE TABLE FICHA(
          ID_PLANTA INT  NOT NULL,
          FRECUENCIA_RIEGO VARCHAR(20) NOT NULL,   
          TIPO_ILUMINACION VARCHAR(30) NOT NULL ,
          TEMPERATURA INT NOT NULL,
          CONSTRAINT FK_ID_FICHA_PLANTA FOREIGN KEY(ID_PLANTA) REFERENCES PLANTA(ID_PLANTA),
		  CONSTRAINT PK_ID_FICHA PRIMARY KEY(ID_PLANTA)
);
GO

--#################### COMPRA Y SATELITES #######################

CREATE TABLE COMPRA(
          ID_COMPRA INT IDENTITY NOT NULL ,
          FECHA DATE NOT NULL,
          CONSTRAINT PK_IDCOMPRA PRIMARY KEY (ID_COMPRA)
);
GO
CREATE TABLE ITEM_COMPRA(
        ID_COMPRA INT NOT NULL, 
        ID_ITEM INT IDENTITY(1, 1) NOT NULL ,
        ID_PLANTA  INT NOT NULL,
        CANTIDAD INT NOT NULL, 
        PRECIO_UNITARIO INT NOT NULL,
        CONSTRAINT FK_ID_PLANTA_EN_ITEMCOMPRA FOREIGN KEY(ID_PLANTA) REFERENCES PLANTA(ID_PLANTA),
        CONSTRAINT FK_ID_COMPRA_EN_ITEMCOMPRA FOREIGN KEY(ID_COMPRA) REFERENCES COMPRA(ID_COMPRA),
        --CONSTRAINT PK_ID_COMPRA PRIMARY KEY(ID_COMPRA,ID_PLANTA)--PARA QUE COMPRA TENGA VARIOS ITEMS 
        CONSTRAINT PK_ID_ITEM_COMPRA PRIMARY KEY(ID_ITEM)
);
GO


CREATE TABLE PLAZA(
        ID_COMPRA INT NOT NULL,
        COSTO_FLETE INT NOT NULL,
        CONSTRAINT FK_IDCOMPRA_PLAZA FOREIGN KEY(ID_COMPRA) REFERENCES COMPRA(ID_COMPRA)
);
GO

CREATE TABLE IMPORTACION(
        ID_COMPRA INT NOT NULL,
        TASA_ARANCEL INT NOT NULL,
        DESCRIPCION_MEDIDAS_SANITARIAS VARCHAR(200) NOT NULL,
        CONSTRAINT FK_IDCOMPRA_IMPORTACION FOREIGN KEY(ID_COMPRA) REFERENCES COMPRA(ID_COMPRA)
);
GO

CREATE TABLE PARAMETRO(
        NOMBRE VARCHAR(100) UNIQUE NOT NULL,
        VALOR VARCHAR(100) NOT NULL,
        CONSTRAINT PK_ID_PARAMETRO PRIMARY KEY(NOMBRE)
);
GO


INSERT INTO PARAMETRO VALUES('DESCRIPCIONT','200')
INSERT INTO PARAMETRO VALUES('DESCRIPCIONP','500')

delete PARAMETRO where NOMBRE = 'DESCRIPCIONT'

update  PARAMETRO set VALOR = '2' where nombre = 'DESCRIPCIONT'

--####################   USUARIO      #########################
CREATE TABLE USUARIO(
            ID INT NOT NULL IDENTITY,
            EMAIL VARCHAR(30) UNIQUE NOT NULL,
            CLAVE varchar(20) NOT NULL,
            CONSTRAINT PK_USUARIO PRIMARY KEY (ID)
);
GO

CREATE TABLE NOMBRE(
ID_PLANTA INT NOT NULL,
NOMBRE_VULGAR VARCHAR(50) NOT NULL,
constraint FK_NOMBRE FOREIGN  KEY(ID_PLANTA) REFERENCES PLANTA(ID_PLANTA),
CONSTRAINT PK_NOMBRE PRIMARY KEY(NOMBRE_VULGAR,ID_PLANTA)
);
--##############################  insercion de valores  ########################################




SELECT * FROM USUARIO
SELECT * FROM PARAMETRO
SELECT * FROM TIPO_PLANTA
SELECT * FROM COMPRA
SELECT * FROM ITEM_COMPRA
SELECT * FROM IMPORTACION
SELECT * FROM PLAZA
SELECT * FROM FICHA
SELECT * FROM PLANTA


--#################  precargas #######################################

SELECT CAST(Scope_Identity() as int)
SELECT * FROM PLANTA WHERE NOMBRE_VULGAR LIKE '%t%' or NOMBRE_CIENTIFICO LIKE '%pl%'


insert TIPO_PLANTA values('Cactáceas','Presencia de espinas, ausencia de hojas y su constitución gruesa, todo lo cual contribuye a la acumulación de agua dentro de sus tejidos para resistir las sequías.'),
                                  ('Conflor','Se reproducen por esporas'),
                                  ('Sinflor','Se reproducen por semillas'),
                                  ('Matas','La mata es un subarbusto o arbusto enano que se distingue del arbusto por la disposición de las ramas a ras del suelo, y por tener menor altura (no suelen superar los 20 cm).'),
                                  ('Hierbas','Es una planta que no presenta órganos decididamente leñosos. Los tallos de las hierbas son verdes y generalmente mueren al acabar la buena estación, siendo luego reemplazados por otros nuevos.');

insert PLANTA (NOMBRE_VULGAR,NOMBRE_CIENTIFICO,DESCRIPCION,ALTURA_MAX, AMBIENTE, CONTINENTEORIGEN ,FOTO ,ID_TIPO ) 
			 values('Nopal','Opuntia ficus-indica','Es una especie arbustiva del género Opuntia de la familia de las cactáceas',9,'Mixta','si','Opuntia_ficus-indica.jpg',1),
                    ('Crotón ','odiaeum veriegatum',' La planta necesita una buena exposición a la luz (pero sin sol directo). De hecho, cuanta más luminosidad reciba, más color tendrán sus hojas.',7,'Mixta','si','Codiaeum_veriegatum.jpg',3),
                    ('Peperomia ','Peperomia obtusifolia variegata','Se trata de una planta sensible al frío, por lo que debes procurar que la temperatura en el interior sea constante. Búscale un rincón cálido y lejos de una ventana, ya que no le gusta recibir los rayos directos del sol.',3,'Interior','si','Peperomia_obtusifolia_variegata.jpg',3),
                    ('Oreja de elefante','Alocasia sanderiana','Es una planta de origen tropical que necesita un riego abundante y muy constante, por lo que tendrás que proporcionarle agua con frecuencia',4,'Interior','si','Alocasia_sanderiana.jpg',3),
                    ('orquídeas mariposa','Orquídeas phalaenopsis','Estas orquídeas deben su nombre a su similitud con las mariposas: "Phalaena = mariposa" y "Opsis= parecido".',5,'Interior','si','Orquídeas_phalaenopsis.jpg',2),
                    ('espatifilo','Spathiphyllum Wallisii','El espatifilo, además de por su valor estético, es una planta que produce mucho interés porque ayuda a purificar el ambiente de los hogares.',7,'Interior','si','Spathiphyllum_Wallisii.jpg',2),
                    ('Romero','Salvia rosmarinus','El romero es una de las plantas medicinales que no pueden faltar en el hogar. Aunque se destaca por su aroma y su uso se extiende en la cocina como condimento.',4,'Exterior','si','Salvia_rosmarinus.jpg',4),
                    ('Tomillo','Thymus','Es una planta aromática que proviene del norte de áfrica y del mediterráneo. Su hoja y flora sirve como medicina y condimento.',3,'Exterior','si','Thymus.jpg',4),
                    ('Trigo','Triticum','El trigo es una planta gramínea con espigas de cuyos granos molidos se saca la harina. La forma del grano de trigo es ovalada con extremos redondeados',10,'Exterior','si','Triticum.jpg',5),      
                    ('Diente de leon','Taraxacum officinale','El diente de león se caracteriza por ser una planta herbácea que crece con facilidad en climas templados. En la medicina tradicional ocupa un lugar destacado por sus aportes al tratamiento de problemas estomacales',8,'Exterior','si','Taraxacum_officinale.jpg',5);

            insert FICHA values(1,'3 por semana','Sol',5),
                                (2,'2 por semana','Sol indirecto',5),
                                (3,'3 por semana','Sol indirecto',23),
                                (4,'5 por semana','Sol indirecto',23),
                                (5,'2 por semana','Sol indirecto',19),
                                (6,'3 por semana','Sol indirecto',18),
                                (7,'2 por semana','Sol',24),
                                (8,'2 por semana','Sol',24),
                                (9,'2 por semana','Sol',17),
                                (10,'2 por semana','Sol',15);
                              

			
			insert USUARIO values('juan@gmail.com','Juan12345'),
                                  ('eduard@gmail.com','Eduard123'),
                                  ('ivan@gmail.com','Ivan12345');

			SELECT DISTINCT PLANTA.*, FICHA.* FROM PLANTA, FICHA 
			WHERE PLANTA.ID_PLANTA = FICHA.ID_PLANTA 

