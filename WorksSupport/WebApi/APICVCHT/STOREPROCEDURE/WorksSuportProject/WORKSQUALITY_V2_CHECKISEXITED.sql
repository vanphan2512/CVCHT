CREATE OR REPLACE PROCEDURE ERP.WORKSQUALITY_V2_CHECKISEXITED
/*	CREATED BY	:	NGUYEN VAN HUAN
	DATE		:	21.6.2017
	DESCRIPTION	:	KIEM TRA TEN TRUNG NHAU 
*/
(	
  V_WORKSSUPPORTQUALITYID IN EO_WORKSSUPPORTQUALITY.WORKSSUPPORTQUALITYID%TYPE,
	V_WORKSSUPPORTQUALITYNAME IN EO_WORKSSUPPORTQUALITY.WORKSSUPPORTQUALITYNAME%TYPE,	
  V_ORDERINDEX IN EO_WORKSSUPPORTQUALITY.ORDERINDEX%TYPE,		
	V_OUT OUT SYS_REFCURSOR
) 
AS
 T_CHECK_NAME NUMBER := 0;
 T_CHECK_ORDER NUMBER := -1;
BEGIN

  BEGIN
    SELECT count(1) INTO T_CHECK_NAME
    FROM EO_WORKSSUPPORTQUALITY EW
    WHERE UPPER(WORKSSUPPORTQUALITYNAME)  = UPPER(V_WORKSSUPPORTQUALITYNAME)
      AND WORKSSUPPORTQUALITYID <> V_WORKSSUPPORTQUALITYID
      AND ISDELETED =0; 
    EXCEPTION WHEN OTHERS THEN T_CHECK_NAME := 0;
  END; 

 -- CHECK ORDERINDEX EXISTED
  IF (V_ORDERINDEX > 0) THEN
   BEGIN
    SELECT count(1) INTO T_CHECK_ORDER
    FROM EO_WORKSSUPPORTQUALITY EW
    WHERE ORDERINDEX = V_ORDERINDEX  AND ISDELETED =0 ;
    EXCEPTION WHEN OTHERS THEN T_CHECK_ORDER := 0;
  END; 
  END IF;  
   -- RETURN TABLE
  OPEN V_OUT FOR
    SELECT T_CHECK_NAME AS WORKSSUPPORTQUALITYNAME, T_CHECK_ORDER AS ORDERINDEX
      FROM dual d;
  RETURN;    


END;
/