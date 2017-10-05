CREATE OR REPLACE PROCEDURE ERP.WORKSSUPPORTTYPE_V2_GETALL
/*
	AUTHOR	      :	BYRS
	DATE CREATED  :	APR 20, 2013
	DESCRIPTION   :	
*/
(	
  V_OUT OUT SYS_REFCURSOR
) 
AS
BEGIN
  OPEN V_OUT FOR
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
    EO_WORKSSUPPORTTYPE.CREATEDUSER,
    EO_WORKSSUPPORTTYPE.CREATEDDATE,
    EO_WORKSSUPPORTTYPE.UPDATEDUSER,
    EO_WORKSSUPPORTTYPE.UPDATEDDATE,
    EO_WORKSSUPPORTTYPE.ISDELETED,
    EO_WORKSSUPPORTTYPE.DELETEDUSER,
    EO_WORKSSUPPORTTYPE.DELETEDDATE
  FROM
    EO_WORKSSUPPORTTYPE
  WHERE
    --EO_WORKSSUPPORTTYPE.ISACTIVE = 1 AND
     EO_WORKSSUPPORTTYPE.ISDELETED = 0
    ORDER BY EO_WORKSSUPPORTTYPE.ORDERINDEX ASC;
END;
/