package com.example.a301265963.myapplication.db;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;

import java.util.ArrayList;
import java.util.Calendar;
import java.util.List;

/**
 * Created by 301265963 on 15/03/2017.
 */

public class MyDB extends SQLiteOpenHelper {

    // All Static variables
    // Database Version
    private static final int DATABASE_VERSION = 1;

    // Database Name
    private static final String DATABASE_NAME = "contactsManager";

    // Contacts table name
    private static final String TABLE_CONTACTS = "contacts";

    // Contacts Table Columns names
    private static final String KEY_ID = "id";
    private static final String KEY_AMOUNT = "amount";
    private static final String KEY_C_ID = "category_id";
    private static final String KEY_NAME = "name";

    public MyDB(Context context) {
        super(context, "ProjectDB", null, DATABASE_VERSION);
    }

    /////////////
    @Override
    public void onCreate(SQLiteDatabase db) {
        String CREATE_PRODUCT_TABLE = "CREATE TABLE " + "productTable" + "("
                + "pId" + " INTEGER PRIMARY KEY," + "pName" + " TEXT,"
                + "pPrice" + " INTEGER," + "pImageId" + " INTEGER)";

        String CREATE_ORDERS_TABLE = "CREATE TABLE " + "OrderTable" + "("
                + "orderId" + " INTEGER PRIMARY KEY," + "name" + " TEXT,"
                + "startTime" + " TEXT," + "endTime" + " TEXT,"+"tableId"+" TEXT,"+"numOfPeople INTEGER,totalPrice INTEGER,isServed INTEGER)";

        String CREATE_TABLE_TABLE = "CREATE TABLE " + "tableTable" + "("
                + "id" + " INTEGER PRIMARY KEY," + "places" + " INTEGER,"
                + "isFull" + " INTEGER)";

        db.execSQL(CREATE_PRODUCT_TABLE);
        db.execSQL(CREATE_TABLE_TABLE);
        db.execSQL(CREATE_ORDERS_TABLE);
      // db.close();



    }

    // Upgrading database
    @Override
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
        // Drop older table if existed
        db.execSQL("DROP TABLE IF EXISTS " + TABLE_CONTACTS);
        // Create tables again
        onCreate(db);
    }
    ////////////

    public void addProduct(int pId, String productName, int productPrice, int pImageId) {
        SQLiteDatabase db = this.getWritableDatabase();

        ContentValues values = new ContentValues();
        values.put("pId", pId);
        values.put("pName", productName);
        values.put("pPrice", productPrice);
        values.put("pImageId", pImageId);
        db.insert("productTable", null, values);
        db.close();
    }

  /*  int orderId;
    String name;
    String startTime;
    String endTime;
    int tableId;
    int numOfpeople;
    int totalPrice;
    int isServed;*/

    public void addTable(int id,  int places, int isFull) {
        SQLiteDatabase db = this.getWritableDatabase();

        ContentValues values = new ContentValues();
        values.put("id", id);
        values.put("places", places);
        values.put("isFull", isFull);

        db.insert("tableTable", null, values);
        db.close();
    }

    public void addOrder( String name, int tableId, int numOfpeople, int totalPrice) {
        SQLiteDatabase db = this.getWritableDatabase();

        ContentValues values = new ContentValues();
        values.put("name", name);
        values.put("tableId", tableId);
        values.put("numOfpeople", numOfpeople);
        values.put("totalPrice", totalPrice);
        values.put("startTime", Calendar.DATE+"");
        values.put("isServed",0);


        db.insert("OrderTable", null, values);
        db.close();
    }

    /*public void addTable(String name,long category) {
        SQLiteDatabase db = this.getWritableDatabase();

        ContentValues values = new ContentValues();
        values.put(KEY_NAME, name);
        values.put(KEY_C_ID, category);
        values.put(KEY_AMOUNT, 0);

        db.insert(TABLE_CONTACTS, null, values);
        db.close();
    }*/

    public List<Table> getTables() {
        List<Table> contactList = new ArrayList<Table>();

        String selectQuery = "SELECT  * FROM " + "tableTable";


        SQLiteDatabase dbb = this.getReadableDatabase();

        Cursor cursor = dbb.rawQuery(selectQuery, null);

        // looping through all rows and adding to list
        if (cursor.moveToFirst()) {//if table not empty
            do {
                int id=cursor.getInt(cursor.getColumnIndex("id"));
//                int id=cursor.getInt(0);
                int places=cursor.getInt(cursor.getColumnIndex("places"));
                int isFull=cursor.getInt(cursor.getColumnIndex( "isFull"));
                //if(name.startsWith("M"))
                //{
                contactList.add(new Table(id,places,isFull));
                //}
            } while (cursor.moveToNext());
        }

        // return contact list
        return contactList;
    }

    public List<Orders> getOrders() {
        List<Orders> contactList = new ArrayList<Orders>();

        String selectQuery = "SELECT  * FROM " + "OrderTable";


        SQLiteDatabase dbb = this.getReadableDatabase();

        Cursor cursor = dbb.rawQuery(selectQuery, null);

        // looping through all rows and adding to list
        if (cursor.moveToFirst()) {//if table not empty
            do {
                int id=cursor.getInt(cursor.getColumnIndex("orderId"));
//                int id=cursor.getInt(0);
                String name = cursor.getString(cursor.getColumnIndex("name"));
                String startTime = cursor.getString(cursor.getColumnIndex("startTime"));
                String endTime = cursor.getString(cursor.getColumnIndex("endTime"));
                int tableId=cursor.getInt(cursor.getColumnIndex("tableId"));
                int numOfPeople=cursor.getInt(cursor.getColumnIndex( "numOfPeople"));
                int totalPrice=cursor.getInt(cursor.getColumnIndex( "totalPrice"));
                int isServed=cursor.getInt(cursor.getColumnIndex( "isServed"));
                //if(name.startsWith("M"))
                //{
                contactList.add(new Orders(id, name,  startTime, endTime, tableId, numOfPeople,totalPrice, isServed));
                //}
            } while (cursor.moveToNext());
        }



        // return contact list
        return contactList;
    }

    public List<Orders> getOrdersNotLeave() {
        List<Orders> contactList = new ArrayList<Orders>();

        String selectQuery = "SELECT  * FROM " + "OrderTable";


        SQLiteDatabase dbb = this.getReadableDatabase();

        Cursor cursor = dbb.rawQuery(selectQuery, null);

        // looping through all rows and adding to list
        if (cursor.moveToFirst()) {//if table not empty
            do {
                int id=cursor.getInt(cursor.getColumnIndex("orderId"));
//                int id=cursor.getInt(0);
                String name = cursor.getString(cursor.getColumnIndex("name"));
                String startTime = cursor.getString(cursor.getColumnIndex("startTime"));
                String endTime = cursor.getString(cursor.getColumnIndex("endTime"));
                int tableId=cursor.getInt(cursor.getColumnIndex("tableId"));
                int numOfPeople=cursor.getInt(cursor.getColumnIndex( "numOfPeople"));
                int totalPrice=cursor.getInt(cursor.getColumnIndex( "totalPrice"));
                int isServed=cursor.getInt(cursor.getColumnIndex( "isServed"));
                if(endTime==null && isServed==1)
                    contactList.add(new Orders(id, name,  startTime, endTime, tableId, numOfPeople,totalPrice, isServed));

            } while (cursor.moveToNext());
        }



        // return contact list
        return contactList;
    }

    public List<Orders> getOrdersNotServed() {
        List<Orders> contactList = new ArrayList<Orders>();

        String selectQuery = "SELECT  * FROM " + "OrderTable";


        SQLiteDatabase dbb = this.getReadableDatabase();

        Cursor cursor = dbb.rawQuery(selectQuery, null);

        // looping through all rows and adding to list
        if (cursor.moveToFirst()) {//if table not empty
            do {
                int id=cursor.getInt(cursor.getColumnIndex("orderId"));
//                int id=cursor.getInt(0);
                String name = cursor.getString(cursor.getColumnIndex("name"));
                String startTime = cursor.getString(cursor.getColumnIndex("startTime"));
                String endTime = cursor.getString(cursor.getColumnIndex("endTime"));
                int tableId=cursor.getInt(cursor.getColumnIndex("tableId"));
                int numOfPeople=cursor.getInt(cursor.getColumnIndex( "numOfPeople"));
                int totalPrice=cursor.getInt(cursor.getColumnIndex( "totalPrice"));
                int isServed=cursor.getInt(cursor.getColumnIndex( "isServed"));
                if(isServed==0)
                contactList.add(new Orders(id, name,  startTime, endTime, tableId, numOfPeople,totalPrice, isServed));
            } while (cursor.moveToNext());
        }



        // return contact list
        return contactList;
    }

    String CREATE_ORDERS_TABLE = "CREATE TABLE " + "OrderTable" + "("
            + "orderId" + " INTEGER PRIMARY KEY," + "name" + " TEXT,"
            + "startTime" + " TEXT," + "endTime" + " TEXT,"+"tableId"+" TEXT,"+"numOfPeople INTEGER,totalPrice INTEGER,isServed INTEGER)";

    public void deletePerson(int id) {
        SQLiteDatabase db = this.getWritableDatabase();
        db.delete(TABLE_CONTACTS, KEY_ID + " = ? or "+KEY_NAME+"= ?", new String[]{id+"","Moshe"});
        db.close();
    }

/////////////////////
    public void updateProductAmount (int id, int amount) {
        SQLiteDatabase db = this.getWritableDatabase();

        ContentValues args = new ContentValues();
        args.put(KEY_AMOUNT, amount);
        db.update(TABLE_CONTACTS, args, KEY_ID + " = ?", new String[]{String.valueOf(id)});
        db.close();
    }


    public void updateTable (int id, int flag) {
        SQLiteDatabase db = this.getWritableDatabase();

        ContentValues args = new ContentValues();
        args.put("isFull", flag);
        db.update("tableTable", args, "id" + " = ?", new String[]{String.valueOf(id)});
        db.close();
    }

    public void updateEndTimeOrder(int id,String endHour){
        String day=endHour.substring(8,9);
        String yaer=endHour.substring(30,33);
        String date=day+"-01-"+yaer;

        SQLiteDatabase db = this.getWritableDatabase();

        ContentValues args = new ContentValues();
        args.put("endTime", date);
        db.update("OrderTable", args, "orderId" + " = ?", new String[]{String.valueOf(id)});
        db.close();

    }

    public void updateIsServedOrder(int id){
        SQLiteDatabase db = this.getWritableDatabase();

        ContentValues args = new ContentValues();
        args.put("isServed", 1);
        db.update("OrderTable", args, "orderId" + " = ?", new String[]{String.valueOf(id)});
        db.close();
    }



    ////////////////
    public List<Product> getProducts() {
        List<Product> contactList = new ArrayList<Product>();

        String selectQuery = "SELECT  * FROM " + "productTable";


        SQLiteDatabase dbdd = this.getReadableDatabase();

        Cursor cursor = dbdd.rawQuery(selectQuery, null);

        // looping through all rows and adding to list
        if (cursor.moveToFirst()) {//if table not empty
            do {
                int id=cursor.getInt(cursor.getColumnIndex("pId"));
//                int id=cursor.getInt(0);
                String name=cursor.getString(cursor.getColumnIndex( "pName"));
                int price=cursor.getInt(cursor.getColumnIndex("pPrice"));
                int img=cursor.getInt(cursor.getColumnIndex( "pImageId"));
               //if(name.startsWith("M"))
               //{
                   contactList.add(new Product(id,name,img,price,0));
               //}
            } while (cursor.moveToNext());
        }

        // return contact list
        return contactList;
    }



    public List<Product> getProductsByCategory (long c_id) {
        List<Product> contactList = new ArrayList<Product>();

        String selectQuery = "SELECT  * FROM " + TABLE_CONTACTS;

        SQLiteDatabase db = this.getReadableDatabase();

        Cursor cursor = db.rawQuery(selectQuery, null);

        // looping through all rows and adding to list
        if (cursor.moveToFirst()) {//if table not empty
            do {
                int id=cursor.getInt(cursor.getColumnIndex(KEY_ID));
//                int id=cursor.getIn
// t(0);
                String name=cursor.getString(cursor.getColumnIndex(KEY_NAME));
                int amount=cursor.getInt(cursor.getColumnIndex(KEY_AMOUNT));
                int category_id=cursor.getInt(cursor.getColumnIndex(KEY_C_ID));
                if(category_id==c_id)
               {
                contactList.add(new Product(id,name,amount,category_id,0));
                }
            } while (cursor.moveToNext());
        }

        // return contact list
        return contactList;
    }

/////////////
    public List<Product> getProductsToShop () {
        List<Product> contactList = new ArrayList<Product>();

        String selectQuery = "SELECT  * FROM " + TABLE_CONTACTS;

        SQLiteDatabase db = this.getReadableDatabase();

        Cursor cursor = db.rawQuery(selectQuery, null);

        // looping through all rows and adding to list
        if (cursor.moveToFirst()) {//if table not empty
            do {
                int id=cursor.getInt(cursor.getColumnIndex(KEY_ID));
//                int id=cursor.getInt(0);
                String name=cursor.getString(cursor.getColumnIndex(KEY_NAME));
                int amount=cursor.getInt(cursor.getColumnIndex(KEY_AMOUNT));
                int category_id=cursor.getInt(cursor.getColumnIndex(KEY_C_ID));
                if(amount>0)
                {
                    contactList.add(new Product(id,name,amount,category_id,0));
                }
            } while (cursor.moveToNext());
        }

        // return contact list
        return contactList;
    }

}
