create or replace PROCEDURE WORKSSUPPORTTYPE_V2_SRH_PAGE
/*
	Created by	:	Ho�ng Nh� Phong 
	Date		:	02/03/2013
	Description	:	N?p danh s�ch Lo?i c�ng vi?c c?n h? tr? 
*/
(
    v_Out OUT SYS_REFCURSOR,
    v_KeyWords NVARCHAR2 DEFAULT NULL,	
    v_PageSize NUMBER DEFAULT 10,
    v_PageIndex NUMBER DEFAULT -1,
    v_IsDeleted IN EO_WORKSSUPPORTTYPE.isdeleted%TYPE DEFAULT -1
     
) 
AS
BEGIN
    OPEN v_Out FOR
    	SELECT *
  FROM(
	SELECT	
		EO_WORKSSUPPORTTYPE.WORKSSUPPORTTYPEID,
		EO_WORKSSUPPORTTYPE.WORKSSUPPORTTYPENAME,
		EO_WORKSSUPPORTTYPE.ICONURL,
		EO_WORKSSUPPORTTYPE.ADDFUNCTIONID,
		EO_WORKSSUPPORTTYPE.VIEWALLFUNCTIONID,
		EO_WORKSSUPPORTTYPE.EDITFUNCTIONID,
		EO_WORKSSUPPORTTYPE.EDITALLFUNCTIONID,
		EO_WORKSSUPPORTTYPE.DELETEFUNCTIONID,
		EO_WORKSSUPPORTTYPE.DELETEALLFUNCTIONID,
		EO_WORKSSUPPORTTYPE.PROCESSFUNCTIONID,
		EO_WORKSSUPPORTTYPE.COMMENTFUNCTIONID,
		EO_WORKSSUPPORTTYPE.DESCRIPTION,
		EO_WORKSSUPPORTTYPE.ORDERINDEX,
		EO_WORKSSUPPORTTYPE.ISACTIVE,
		EO_WORKSSUPPORTTYPE.ISSYSTEM,		
		EO_WORKSSUPPORTTYPE.CREATEDDATE,
		EO_WORKSSUPPORTTYPE.UPDATEDUSER,
		EO_WORKSSUPPORTTYPE.UPDATEDDATE,
		EO_WORKSSUPPORTTYPE.ISDELETED,
		EO_WORKSSUPPORTTYPE.DELETEDUSER,
		EO_WORKSSUPPORTTYPE.DELETEDDATE,
    CUS.USERNAME ||'-'||CUS.FULLNAME CREATEDUSER,
    ROW_NUMBER() OVER (ORDER BY  EO_WORKSSUPPORTTYPE.workssupporttypeid ASC) STT,
    SUM(1) over()AS TOTALROWS
	FROM EO_WORKSSUPPORTTYPE
  	LEFT JOIN SYS_USER CUS ON CUS.USERNAME = EO_WORKSSUPPORTTYPE.CREATEDUSER	
	WHERE
  (v_KeyWords IS NULL  OR  UPPER(EO_WORKSSUPPORTTYPE.WORKSSUPPORTTYPENAME) LIKE '%'||UPPER(v_KeyWords)||'%')
	 AND (v_IsDeleted = -1 OR EO_WORKSSUPPORTTYPE.isdeleted=v_IsDeleted)	
 )
  WHERE
      (STT > v_PageIndex * v_PageSize AND STT <= (v_PageIndex + 1) * v_PageSize)
        OR v_PageIndex = -1
;

END;
