CREATE TABLE TMP_USER.APP_TABLE
(
	APP_ID				NUMBER,
	APP_DESCRIPTION		VARCHAR2(100),
	CREATED_DATE		DATE,
	CREATED_BY			VARCHAR2(30),

	CONSTRAINT APP_TABLE_PK PRIMARY KEY (APP_ID)
);