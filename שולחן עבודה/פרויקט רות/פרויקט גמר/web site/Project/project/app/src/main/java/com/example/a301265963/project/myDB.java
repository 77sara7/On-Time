package com.example.a301265963.project;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;

import com.example.a301265963.project.orders.Order;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;


public class myDB extends SQLiteOpenHelper {


        private static final int DATABASE_VERSION = 11;

        // Database Name
        private static final String DATABASE_NAME = "My_Products";

        // Contacts table name
        private static final String TABLE_TABLES = "Tables";

        // Contacts Table Columns names
        private static final String KEY_ID = "id";
        private static final String KEY_PLACES = "places";
        private static final String KEY_IS_FULL = "isFull";


        private static final String TABLE_ORD = "Orders";

        // Contacts Table Columns names
        private static final String KEY_ORDER_ID = "orderId";
        private static final String KEY_NAME = "name";
        private static final String KEY_START_TIME = "startTime";
        private static final String KEY_END_TIME = "endTime";
        private static final String KEY_TABLE_ID = "tableId";
        private static final String KEY_NUM_OF_PEOPLE = "numOfPeople";
        private static final String KEY_TOTAL_PRICE = "totalPrice";
        private static final String KEY_IS_SERVED = "isServed";


        private static final String TABLE_PRODUCT = "Products";

        // Contacts Table Columns names
        private static final String KEY_PRODUCT_ID = "productId";
        private static final String KEY_PRODUCT_NAME = "productName";
        private static final String KEY_PRICE = "productPrice";
        private static final String KEY_IMAGE_ID = "productImageId";

        public myDB(Context context) {
            super(context, DATABASE_NAME, null, DATABASE_VERSION);
        }

        // Creating Tables
        @Override
        public void onCreate(SQLiteDatabase db) {


            String CREATE_TABLES_TABLE = "CREATE TABLE " + TABLE_TABLES + "("
                    + KEY_ID + " INTEGER PRIMARY KEY," + KEY_PLACES + "  INTEGER," + KEY_IS_FULL + " TEXT"
                    + ")";
            String CREATE_ORD_TABLE = "CREATE TABLE " + TABLE_ORD + "("
                    + KEY_ORDER_ID + " INTEGER PRIMARY KEY," + KEY_NAME + "  TEXT," + KEY_START_TIME + " TEXT,"
                    + KEY_END_TIME + " TEXT," + KEY_TABLE_ID + " INTEGER," + KEY_NUM_OF_PEOPLE + " INTEGER," + KEY_TOTAL_PRICE + " INTEGER,"
                    + KEY_IS_SERVED + " TEXT"
                    + ")";
            String CREATE_PRODUCT_TABLE = "CREATE TABLE " + TABLE_PRODUCT + "("
                    + KEY_PRODUCT_ID + " INTEGER PRIMARY KEY," + KEY_PRODUCT_NAME + "  TEXT," + KEY_PRICE + " INTEGER," + KEY_IMAGE_ID + " INTEGER"
                    + ")";
            db.execSQL(CREATE_TABLES_TABLE);
            db.execSQL(CREATE_ORD_TABLE);
            db.execSQL(CREATE_PRODUCT_TABLE);
//        addTable(1,false,10);
//        addTable(2,true,9);
        }

        public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
            // Drop older table if existed
            db.execSQL("DROP TABLE IF EXISTS " + TABLE_ORD);
            db.execSQL("DROP TABLE IF EXISTS " + TABLE_PRODUCT);
            db.execSQL("DROP TABLE IF EXISTS " + TABLE_TABLES);

            // Create tables again
            onCreate(db);
        }

        public void addTable(int tableId , String isFull, int places) {
            SQLiteDatabase db = this.getWritableDatabase();

            //dictionary
            ContentValues values = new ContentValues();
            values.put(KEY_PLACES, places);
            values.put(KEY_IS_FULL, isFull);
            values.put(KEY_ID, tableId);

            // Inserting Row
            db.insert(TABLE_TABLES, null, values);
            db.close();
        }

        public void addProduct(int productId, int productImageId, String productName, int price) {
            SQLiteDatabase db = this.getWritableDatabase();

            //dictionary
            ContentValues values = new ContentValues();
            values.put(KEY_PRODUCT_ID, productId);
            values.put(KEY_IMAGE_ID, productImageId);
            values.put(KEY_PRODUCT_NAME, productName);
            values.put(KEY_PRICE, price);

            // Inserting Row
            db.insert(TABLE_PRODUCT, null, values);
            db.close();
        }

        public void addOrder(int orderId, String name, String startTime, String endTime, int numOfPeople, int tableId, int totalPrice, String isServed) {
            SQLiteDatabase db = this.getWritableDatabase();

            //dictionary

            ContentValues values = new ContentValues();
            values.put(KEY_ORDER_ID, orderId);
            values.put(KEY_NAME, name);
            values.put(KEY_START_TIME, startTime);
            values.put(KEY_END_TIME, endTime);
            values.put(KEY_NUM_OF_PEOPLE, numOfPeople);
            values.put(KEY_TABLE_ID, tableId);
            values.put(KEY_TOTAL_PRICE, totalPrice);
            values.put(KEY_IS_SERVED, isServed);
            // Inserting Row
            db.insert(TABLE_ORD, null, values);
            db.close(); // Closing database connection
            updateIsFullTable(tableId,"true");
        }

        //  delete
        public void deleteProduct(int id) {
            SQLiteDatabase db = this.getWritableDatabase();
            db.delete(TABLE_PRODUCT, KEY_PRODUCT_ID + " = ? ", new String[]{id + ""});
            db.close();
        }

        public void deleteTable(int id) {
            SQLiteDatabase db = this.getWritableDatabase();
            db.delete(TABLE_TABLES, KEY_ID + " = ? ", new String[]{id + ""});
            db.close();
        }

        public void deleteOrder(int id) {
            SQLiteDatabase db = this.getWritableDatabase();
            db.delete(TABLE_TABLES, KEY_ORDER_ID + " = ? ", new String[]{id + ""});
            db.close();
        }

        public void updateTable() {
        }

        public void updateIsFullTable(int id,String isFull) {
            SQLiteDatabase db = this.getWritableDatabase();
            ContentValues args = new ContentValues();
            args.put(KEY_IS_FULL, isFull);
            db.update(TABLE_TABLES, args, KEY_ID + " = ?", new String[]{id + ""});
            db.close();
        }
        public void updateServe(int id) {
            SQLiteDatabase db = this.getWritableDatabase();
            ContentValues args = new ContentValues();
            args.put(KEY_IS_SERVED, "true");
            db.update(TABLE_ORD, args, KEY_ORDER_ID + " = ?", new String[]{id + ""});
            db.close();
        }

        public List<Product> getAllProducts() {
            List<Product> contactProduct = new ArrayList<Product>();

            String select = "SELECT * FROM " + TABLE_PRODUCT;
            SQLiteDatabase db = this.getReadableDatabase();
            Cursor cursor = db.rawQuery(select, null);

            if (cursor.moveToFirst())
                do {

                    int id = cursor.getInt(cursor.getColumnIndex(KEY_PRODUCT_ID));
                    int price = cursor.getInt(cursor.getColumnIndex(KEY_PRICE));
                    String name = cursor.getString(cursor.getColumnIndex(KEY_PRODUCT_NAME));
                    int image = cursor.getInt(cursor.getColumnIndex(KEY_IMAGE_ID));
                    contactProduct.add(new Product(id, price, name, image));
                }

                while (cursor.moveToNext());

            return contactProduct;
        }

        public List<Table> getAllTables() {

            List<Table> ListTable = new ArrayList<Table>();

            String select = "SELECT * FROM " + TABLE_TABLES;
            SQLiteDatabase db = this.getReadableDatabase();
            Cursor cursor = db.rawQuery(select, null);

            if (cursor.moveToFirst())
                do {

                    int id = cursor.getInt(cursor.getColumnIndex(KEY_ID));
                    int numPlaces = cursor.getInt(cursor.getColumnIndex(KEY_PLACES));
                    boolean isfull = Boolean.parseBoolean(cursor.getString(cursor.getColumnIndex(KEY_IS_FULL)));

                    ListTable.add(new Table(id, numPlaces, isfull));
                }

                while (cursor.moveToNext());

            return ListTable;
        }
        public List<Order> getAllOrders() {

            List<Order> ListOrder = new ArrayList<Order>();

            String select = "SELECT * FROM " + TABLE_ORD;
            SQLiteDatabase db = this.getReadableDatabase();
            Cursor cursor = db.rawQuery(select, null);

         if (cursor.moveToFirst())
                do {

                    int id = cursor.getInt(cursor.getColumnIndex(KEY_ORDER_ID));
                    String isServed = cursor.getString(cursor.getColumnIndex(KEY_IS_SERVED));
                    String name = cursor.getString(cursor.getColumnIndex(KEY_NAME));
                    int TableId=cursor.getInt(cursor.getColumnIndex(KEY_TABLE_ID));
                    int people=cursor.getInt(cursor.getColumnIndex(KEY_NUM_OF_PEOPLE));
                    int totalPrice=cursor.getInt(cursor.getColumnIndex(KEY_TOTAL_PRICE));
                    ListOrder.add(new Order(id, isServed,TableId,name,people,totalPrice));
                }

                while (cursor.moveToNext());

            return ListOrder;
        }

        public  void  updateEndOrder(int tableId, Date EndTime){

            SQLiteDatabase db = this.getWritableDatabase();
            ContentValues args = new ContentValues();
            args.put(KEY_END_TIME, EndTime.toString());
            db.update(TABLE_ORD, args, KEY_ORDER_ID + " = ?", new String[]{tableId + ""});
            db.close();
        }
    }



