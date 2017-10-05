create or replace PROCEDURE     WORKSSUPPORTSTATUS_V2_GETALL
(
  V_OUT OUT SYS_REFCURSOR
)
AS
BEGIN
  OPEN V_OUT FOR
  SELECT
    EO_WORKSSUPPORTSTATUS.WORKSSUPPORTSTATUSID,
    EO_WORKSSUPPORTSTATUS.WORKSSUPPORTSTATUSNAME,
    EO_WORKSSUPPORTSTATUS.ICONURL,
    EO_WORKSSUPPORTSTATUS.ISINITSTATUS,
    EO_WORKSSUPPORTSTATUS.ISCOMPLETESTATUS,
    EO_WORKSSUPPORTSTATUS.ISCLOSESTATUS,
    EO_WORKSSUPPORTSTATUS.DESCRIPTION,
    EO_WORKSSUPPORTSTATUS.ORDERINDEX,
    EO_WORKSSUPPORTSTATUS.ISACTIVE,
    EO_WORKSSUPPORTSTATUS.ISSYSTEM    
  FROM
    ERP.EO_WORKSSUPPORTSTATUS
  WHERE
   -- EO_WORKSSUPPORTSTATUS.ISACTIVE = 1AND 
    EO_WORKSSUPPORTSTATUS.ISDELETED = 0

  ORDER BY
    EO_WORKSSUPPORTSTATUS.ORDERINDEX
  ;
END;
