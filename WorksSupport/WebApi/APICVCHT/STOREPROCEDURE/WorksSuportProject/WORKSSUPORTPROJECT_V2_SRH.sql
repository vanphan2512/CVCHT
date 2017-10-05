CREATE OR REPLACE PROCEDURE ERP.WORKSSUPORTPROJECT_V2_SRH
/*
	CREATED BY	:	Nguy?n Van Ph?n
	DATE		:	09/06/2017
	DESCRIPTION	:	N?P DANH S�CH D? �N

*/
(
    V_OUT OUT SYS_REFCURSOR,
    V_KEYWORDS NVARCHAR2 DEFAULT NULL,
    V_ISDELETED EO_WORKSSUPPORTPROJECT.ISDELETED%TYPE DEFAULT 0,
    V_PAGEINDEX NUMBER DEFAULT -1,
    V_PAGESIZE NUMBER DEFAULT 500
) 
AS
BEGIN
   OPEN V_OUT FOR
   SELECT * 
   FROM(
	SELECT
	  EO_WORKSSUPPORTPROJECT.WORKSSUPPORTPROJECTID,
		EO_WORKSSUPPORTPROJECT.WORKSSUPPORTPROJECTNAME,
    EO_WORKSSUPPORTPROJECT.WORKSSUPPORTTYPEID,
		EO_WORKSSUPPORTPROJECT.DESCRIPTION,
		EO_WORKSSUPPORTPROJECT.ORDERINDEX,
		EO_WORKSSUPPORTPROJECT.ISACTIVE,
		EO_WORKSSUPPORTPROJECT.ISSYSTEM,
    EO_WORKSSUPPORTPROJECT.ICONURL,
		NVL2(EO_WORKSSUPPORTPROJECT.CREATEDUSER,	EO_WORKSSUPPORTPROJECT.CREATEDUSER ||'-'||CREATEDUSER.FULLNAME,'') CREATEDUSER,
		EO_WORKSSUPPORTPROJECT.CREATEDDATE,
		NVL2( EO_WORKSSUPPORTPROJECT.UPDATEDUSER,EO_WORKSSUPPORTPROJECT.UPDATEDUSER ||'-'||UPDATEDUSER.FULLNAME,'') UPDATEDUSER,
		EO_WORKSSUPPORTPROJECT.UPDATEDDATE,
		EO_WORKSSUPPORTPROJECT.ISDELETED,
		EO_WORKSSUPPORTPROJECT.DELETEDUSER,
		EO_WORKSSUPPORTPROJECT.DELETEDDATE
  
	FROM 
    EO_WORKSSUPPORTPROJECT
    LEFT  JOIN SYS_USER CREATEDUSER 
      ON CREATEDUSER.USERNAME = EO_WORKSSUPPORTPROJECT.CREATEDUSER
    LEFT  JOIN SYS_USER UPDATEDUSER 
      ON UPDATEDUSER.USERNAME = EO_WORKSSUPPORTPROJECT.UPDATEDUSER
	WHERE 
    EO_WORKSSUPPORTPROJECT.ISDELETED = V_ISDELETED
    AND (V_KEYWORDS IS NULL 
         OR EO_WORKSSUPPORTPROJECT.WORKSSUPPORTPROJECTID LIKE '%' || V_KEYWORDS || '%'
         OR UPPER ( EO_WORKSSUPPORTPROJECT.WORKSSUPPORTPROJECTNAME ) LIKE '%' ||UPPER ( V_KEYWORDS )|| '%')
  ORDER BY 
    EO_WORKSSUPPORTPROJECT.ISACTIVE DESC
  );
END;
/