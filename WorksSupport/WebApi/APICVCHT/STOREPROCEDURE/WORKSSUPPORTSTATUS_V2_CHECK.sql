CREATE OR REPLACE PROCEDURE ERP.WORKSSUPPORTSTATUS_V2_CHECK
/*	CREATED BY	:	NGUYEN VAN HUAN
	DATE		:	21.6.2017
	DESCRIPTION	:	KIEM TRA TEN TRUNG NHAU TRONG BANG EO_WORKSSUPPORTSTATUS
*/
(	
  V_WORKSSUPPORTSTATUSID IN EO_WORKSSUPPORTSTATUS.workssupportstatusid%TYPE,
	V_WORKSSUPPORTSTATUSNAME IN EO_WORKSSUPPORTSTATUS.workssupportstatusname%TYPE,
  V_ORDERINDEX IN EO_WORKSSUPPORTSTATUS.orderindex%TYPE,
	V_OUT OUT SYS_REFCURSOR
) 
AS
 T_CHECK_NAME NUMBER := 0;
 T_CHECK_ORDER NUMBER := -1;
BEGIN
  -- CHECK WORKSSUPPORTSTATUSNAME EXISTED
  BEGIN
    SELECT count(1) INTO t_check_name
    FROM EO_WORKSSUPPORTSTATUS EO
    WHERE UPPER(EO.WORKSSUPPORTSTATUSNAME) = UPPER(V_WORKSSUPPORTSTATUSNAME) AND eo.workssupportstatusid <> v_workssupportstatusid
    AND EO.isdeleted =0;    
    EXCEPTION WHEN OTHERS THEN T_CHECK_NAME := 0;
  END;

   -- CHECK ORDERINDEX EXISTED
  IF (V_ORDERINDEX > 0) THEN
    BEGIN
      SELECT count(1) INTO t_check_order
      FROM EO_WORKSSUPPORTSTATUS EO
      WHERE EO.orderindex = v_orderindex
      AND EO.isdeleted =0;    
      EXCEPTION WHEN OTHERS THEN T_CHECK_ORDER := 0;
    END;
  END IF;
      
  -- RETURN TABLE
  OPEN V_OUT FOR
    SELECT t_check_name AS STATUSNAME, t_check_order AS ORDERINDEX
      FROM dual d;
  RETURN;    

END;
/