﻿

CREATE TABLE IF NOT EXISTS public."Products"
(
    "Id" integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 9223372036854775807 CACHE 1 ),
      "Name" character varying(150) NOT NULL,
    "Description" character varying(2500),
    "Price" money NOT NULL,
    CONSTRAINT "Products_pkey" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."Products"
    OWNER to postgres;

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."Products"
    OWNER to postgres;