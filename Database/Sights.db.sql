BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "Users" (
	"Id"	INTEGER,
	"Username"	TEXT UNIQUE,
	"Password"	TEXT,
	PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Countries" (
	"Id"	INTEGER,
	"Name"	TEXT UNIQUE,
	"UserId"	INTEGER,
	PRIMARY KEY("Id" AUTOINCREMENT),
	FOREIGN KEY("UserId") REFERENCES "Users"("Id")
);
CREATE TABLE IF NOT EXISTS "Cities" (
	"Id"	INTEGER,
	"Name"	TEXT UNIQUE,
	"UserId"	INTEGER,
	"CountryId"	INTEGER,
	PRIMARY KEY("Id" AUTOINCREMENT),
	FOREIGN KEY("CountryId") REFERENCES "Countries"("Id"),
	FOREIGN KEY("UserId") REFERENCES "Users"("Id")
);
CREATE TABLE IF NOT EXISTS "Attractions" (
	"Id"	INTEGER,
	"Coordinates"	REAL,
	"Title"	TEXT,
	"Description"	TEXT,
	"UserId"	INTEGER,
	"Picture"	BLOB,
	"CountryId"	INTEGER,
	"CityId"	INTEGER,
	PRIMARY KEY("Id" AUTOINCREMENT),
	FOREIGN KEY("UserId") REFERENCES "Users"("Id"),
	FOREIGN KEY("CityId") REFERENCES "Cities"("Id"),
	FOREIGN KEY("CountryId") REFERENCES "Countries"("Id")
);
CREATE TABLE IF NOT EXISTS "Likes" (
	"Id"	INTEGER,
	"UserId"	INTEGER,
	"AttractionId"	INTEGER,
	"Like"	INTEGER,
	PRIMARY KEY("Id" AUTOINCREMENT),
	FOREIGN KEY("AttractionId") REFERENCES "Attractions"("Id"),
	FOREIGN KEY("UserId") REFERENCES "Users"("Id")
);
CREATE TABLE IF NOT EXISTS "Comments" (
	"Id"	INTEGER,
	"UserId"	INTEGER,
	"Content"	TEXT,
	"AttractionId"	INTEGER,
	PRIMARY KEY("Id" AUTOINCREMENT),
	FOREIGN KEY("AttractionId") REFERENCES "Attractions"("Id"),
	FOREIGN KEY("UserId") REFERENCES "Users"("Id")
);
CREATE TABLE IF NOT EXISTS "SubComments" (
	"Id"	INTEGER,
	"CommentId"	INTEGER,
	"UserId"	INTEGER,
	"Content"	TEXT,
	PRIMARY KEY("Id" AUTOINCREMENT),
	FOREIGN KEY("CommentId") REFERENCES "Comments"("Id"),
	FOREIGN KEY("UserId") REFERENCES "Users"("Id")
);
INSERT INTO "Users" VALUES (0,'Maria','HejIgen!');
INSERT INTO "Users" VALUES (1,'Alexandra','HejHej1');
INSERT INTO "Users" VALUES (2,'Jenny','Hejdå123');
INSERT INTO "Users" VALUES (3,'Louise','Julklappar1');
INSERT INTO "Users" VALUES (4,'Martin','Motorcykel1');
INSERT INTO "Users" VALUES (5,'Fredric','Padel1');
INSERT INTO "Countries" VALUES (0,'Denmark',0);
INSERT INTO "Countries" VALUES (1,'Sweden',1);
INSERT INTO "Countries" VALUES (2,'Norway',2);
INSERT INTO "Countries" VALUES (3,'Finland',3);
INSERT INTO "Countries" VALUES (4,'Iceland',0);
INSERT INTO "Cities" VALUES (0,'Copenhagen',0,0);
INSERT INTO "Cities" VALUES (1,'Stockholm',1,1);
INSERT INTO "Cities" VALUES (2,'Oslo',2,2);
INSERT INTO "Cities" VALUES (3,'Helsinki',3,3);
INSERT INTO "Cities" VALUES (4,'Malmö',1,1);
INSERT INTO "Cities" VALUES (5,'Västerås',5,1);
INSERT INTO "Attractions" VALUES (0,NULL,'Tivoli Amusement','A fun amusement park!',0,NULL,0,0);
INSERT INTO "Attractions" VALUES (1,NULL,'Nennes café','A lovely coffee shop, with great ''fika''',1,NULL,1,1);
INSERT INTO "Attractions" VALUES (2,NULL,'Henkes pizza','Great restaurant, with excellent serivce',2,NULL,2,2);
INSERT INTO "Attractions" VALUES (3,NULL,'Borgbacken Amusement','Fun Amusement park',3,NULL,3,3);
INSERT INTO "Attractions" VALUES (4,NULL,'Café Stars','Very cozy café with good service',2,NULL,1,4);
INSERT INTO "Attractions" VALUES (5,NULL,'Cafe vid havet','Säljer god glass',2,NULL,1,4);
INSERT INTO "Likes" VALUES (2,0,0,0);
INSERT INTO "Likes" VALUES (3,2,1,1);
INSERT INTO "Likes" VALUES (4,1,4,1);
INSERT INTO "Likes" VALUES (5,0,1,1);
INSERT INTO "Comments" VALUES (1,0,'I love this place it is sooo goood...',1);
INSERT INTO "Comments" VALUES (2,2,'I also love this place',1);
INSERT INTO "Comments" VALUES (3,1,'I do not like this place!!',1);
COMMIT;
