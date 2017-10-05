CREATE OR REPLACE PROCEDURE ERP.WORKSSUPPORT_MEMBER_V2_SRH
/*
	CREATED BY	:	NGUYEN THI KIM NGAN
	DATE		:	27.06.2017
	DESCRIPTION	:	N?P DANH S�CH TR?NG TH�I C�NG VI?C C?N H? TR? 

*/
(
    V_OUT OUT SYS_REFCURSOR,
    V_KEYWORDS NVARCHAR2 DEFAULT NULL,
    V_ISDELETED EO_WORKSSUPPORT_MEMBER.ISDELETED%TYPE DEFAULT 0,
    V_PAGEINDEX NUMBER DEFAULT -1,
    V_PAGESIZE NUMBER DEFAULT -1
) 
AS
BEGIN
   OPEN V_OUT FOR
   SELECT * 
   FROM(
	SELECT
		EO_WORKSSUPPORT_MEMBER.WORKSSUPPORTID,
		EO_WORKSSUPPORT_MEMBER.WORKSSUPPORTDATE,
		EO_WORKSSUPPORT_MEMBER.MEMBERUSERNAME,
		EO_WORKSSUPPORT_MEMBER.WORKSSUPPORTMEMBERROLEID,
		EO_WORKSSUPPORT_MEMBER.NOTE,
		EO_WORKSSUPPORT_MEMBER.UPDATEDDATE,
		EO_WORKSSUPPORT_MEMBER.ISDELETED,
		EO_WORKSSUPPORT_MEMBER.DELETEDUSER,
		NVL2(EO_WORKSSUPPORT_MEMBER.UPDATEDUSER,	EO_WORKSSUPPORT_MEMBER.UPDATEDUSER ||'-'||EO_WORKSSUPPORT_MEMBER.UPDATEDUSER.FULLNAME,'') UPDATEDUSER,
    ROW_NUMBER() OVER (ORDER BY EO_WORKSSUPPORT_MEMBER.WORKSSUPPORTID ASC ,EO_WORKSSUPPORT_MEMBER.UPDATEDDATE DESC) STT,
    SUM(1) OVER()AS TOTALROWS
	FROM 
    EO_WORKSSUPPORT_MEMBER
    LEFT  JOIN SYS_USER UPDATEDUSER 
      ON UPDATEDUSER.USERNAME = EO_WORKSSUPPORT_MEMBER.UPDATEDUSER
	WHERE 
    EO_WORKSSUPPORT_MEMBER.ISDELETED = V_ISDELETED
    AND (V_KEYWORDS IS NULL 
         OR EO_WORKSSUPPORT_MEMBER.WORKSSUPPORTID LIKE '%' || V_KEYWORDS || '%'
         OR UPPER ( EO_WORKSSUPPORT_MEMBER.MEMBERUSERNAME ) LIKE '%' ||UPPER ( V_KEYWORDS )|| '%')
  ORDER BY 
    EO_WORKSSUPPORT_MEMBER.WORKSSUPPORTID
  )
  WHERE
  V_PAGESIZE < 0 
  OR
  (STT > V_PAGEINDEX * V_PAGESIZE AND STT <= (V_PAGEINDEX + 1) * V_PAGESIZE);
END;
/