CREATE OR REPLACE PROCEDURE ERP.WORKSSUPPORTTYPE_V2_CHK_USED
/*
	CREATED BY	:	NGUYEN VAN HUAN
	DATE		:	17/08/2017
	DESCRIPTION	:	KIEM TRA LOAI CONG VIEC DA DUOC DUNG CHUA.
*/
(   
    V_WORKSSUPPORTTYPEID IN EO_WORKSSUPPORTPROJECT.WORKSSUPPORTTYPEID%TYPE,
    V_OUT OUT NUMBER
)
  AS
  T_NUMBERTYPEID NUMBER := 0;    
  BEGIN
      SELECT COUNT(1) INTO T_NUMBERTYPEID FROM
      EO_WORKSSUPPORTTYPE EW
      WHERE EXISTS (SELECT EW1.WORKSSUPPORTTYPEID FROM EO_WORKSSUPPORTPROJECT EW1
                      WHERE 
                           EW1.WORKSSUPPORTTYPEID = EW.WORKSSUPPORTTYPEID AND
                           EW.WORKSSUPPORTTYPEID = V_WORKSSUPPORTTYPEID AND EW.ISDELETED = 0 AND
                           EW.ISDELETED = 0
                     );
    V_OUT:= T_NUMBERTYPEID;
  END;
/