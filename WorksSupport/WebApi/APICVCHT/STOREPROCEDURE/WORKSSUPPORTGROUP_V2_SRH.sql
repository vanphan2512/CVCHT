create or replace PROCEDURE WORKSSUPPORTGROUP_V2_SRH
/*
	CREATED BY	:	NGUYEN THI KIM NGAN
	DATE		:	08/06/2017
	DESCRIPTION	:	N?P DANH S�CH NH�M C�NG VI?C C?N H? TR? 
*/
(
    V_OUT OUT SYS_REFCURSOR,
    V_KEYWORDS NVARCHAR2 DEFAULT NULL,
    V_ISDELETED EO_WORKSSUPPORTGROUP.ISDELETED%TYPE DEFAULT 0,
    V_PAGEINDEX NUMBER DEFAULT -1,
    V_PAGESIZE NUMBER DEFAULT -1
) 
AS
BEGIN
   OPEN V_OUT FOR
   SELECT * 
   FROM(
	SELECT
    WSG.WORKSSUPPORTGROUPID,
    WSG.WORKSSUPPORTGROUPNAME,
    WSG.WORKSSUPPORTPROJECTID,
    WSG.DESCRIPTION,
    WSG.ORDERINDEX,
    WSG.ISACTIVE,
    WSG.ISSYSTEM,
    NVL2(WSG.CREATEDUSER,	WSG.CREATEDUSER ||'-'||CREATEDUSER.FULLNAME,'') CREATEDUSER,
    WSG.CREATEDDATE,
    NVL2( WSG.UPDATEDUSER,WSG.UPDATEDUSER ||'-'||UPDATEDUSER.FULLNAME,'') UPDATEDUSER,
    ROW_NUMBER() OVER (ORDER BY WSG.ORDERINDEX ASC ,WSG.CREATEDDATE DESC) STT,
    SUM(1) OVER()AS TOTALROWS
	FROM 
    EO_WORKSSUPPORTGROUP WSG
    LEFT  JOIN SYS_USER CREATEDUSER 
      ON CREATEDUSER.USERNAME = WSG.CREATEDUSER
    LEFT  JOIN SYS_USER UPDATEDUSER 
      ON UPDATEDUSER.USERNAME = WSG.UPDATEDUSER
	WHERE 
    WSG.ISDELETED = V_ISDELETED
    AND (V_KEYWORDS IS NULL 
         OR WSG.WORKSSUPPORTGROUPID LIKE '%' || V_KEYWORDS || '%'
         OR UPPER ( WSG.WORKSSUPPORTGROUPNAME ) LIKE '%' ||UPPER ( V_KEYWORDS )|| '%')
  ORDER BY 
    WSG.ORDERINDEX
  )
  WHERE
  V_PAGESIZE < 0 
  OR
  (STT > V_PAGEINDEX * V_PAGESIZE AND STT <= (V_PAGEINDEX + 1) * V_PAGESIZE);
END;