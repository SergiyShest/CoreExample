START TRANSACTION;



ALTER TABLE "Vjsf" ADD "BackGroundColor" text NULL;

ALTER TABLE "Vjsf" ADD "Font" text NULL;

ALTER TABLE "Vjsf" ADD "ForeColor" text NULL;

ALTER TABLE "Vjsf" ADD "NextButtonText" text NULL;

ALTER TABLE "Vjsf" ADD "PrevButtonText" text NULL;


INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20230912023648_AddFields', '7.0.5');

COMMIT;



ALTER TABLE IF EXISTS public."Questionnaire"
    RENAME "Name" TO "Text";


ALTER TABLE IF EXISTS public."Questionnaire"
    RENAME "Code" TO "Name";