export class RequestDto{
    
    request_id:number;
    request_name:string;
    date_from:Date;
    date_to:Date;
    user_id:number;
    day:number;
    hour:number;  
    day_in_month:number;  
    frequency_id:number;  
    file_stream:number[];
    recording_stream:number[];
     //:BinaryType;////לשאול סוג
    file_name:string;
    file_id;  
    recording_id; 
    is_relevant:Boolean;
    IsAuthorized:Boolean;  
    ErrorMessage:string;  
}