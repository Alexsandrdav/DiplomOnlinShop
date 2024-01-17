-- Table: public.OrderProduct

-- DROP TABLE IF EXISTS public."OrderProduct";

CREATE TABLE IF NOT EXISTS public."OrderProduct"
(
    "ProductId" integer NOT NULL,
    "OrderId" integer NOT NULL,
    CONSTRAINT "PK_OrderProduct" PRIMARY KEY ("ProductId", "OrderId"),
    CONSTRAINT "FK_OrderId" FOREIGN KEY ("OrderId")
        REFERENCES public."Orders" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID,
    CONSTRAINT "FK_ProductId" FOREIGN KEY ("ProductId")
        REFERENCES public."Products" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."OrderProduct"
    OWNER to postgres;
-- Index: fki_FK_OrderId

-- DROP INDEX IF EXISTS public."fki_FK_OrderId";

CREATE INDEX IF NOT EXISTS "fki_FK_OrderId"
    ON public."OrderProduct" USING btree
    ("OrderId" ASC NULLS LAST)
    TABLESPACE pg_default;
-- Index: fki_FK_ProductId

-- DROP INDEX IF EXISTS public."fki_FK_ProductId";

CREATE INDEX IF NOT EXISTS "fki_FK_ProductId"
    ON public."OrderProduct" USING btree
    ("ProductId" ASC NULLS LAST)
    TABLESPACE pg_default;