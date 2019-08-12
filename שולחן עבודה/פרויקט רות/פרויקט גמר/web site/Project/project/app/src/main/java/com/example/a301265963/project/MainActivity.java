package com.example.a301265963.project;

import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import  com.example.a301265963.project.ReserountMap;
import  com.example.a301265963.project.orders.OrdersActivity;

import com.example.a301265963.project.newOrder;

import java.util.Date;

public class MainActivity extends AppCompatActivity {
    myDB myDB;
    int OrderId=0;
    Date Startdate;
//pictures
    //orderrrrrrrrrrrrrr add

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        myDB = new myDB(getApplicationContext());
        myDB.addTable(1, "false", 10);
        myDB.addTable(2, "true", 9);
        myDB.addTable(3, "false", 8);
        myDB.addTable(4, "false", 7);
        myDB.addTable(5, "false", 9);
        myDB.addTable(6, "false", 7);
        myDB.addTable(7, "false", 6);

        myDB.addProduct(0, R.drawable.birthdaycake,"birthday cake",150);
        myDB.addProduct(2, R.drawable.burgersandwitch,"burger sandwitch",55);
        myDB.addProduct(3, R.drawable.chinarace,"china race",50);
        myDB.addProduct(4, R.drawable.espresso,"espresso",30);
        myDB.addProduct(5, R.drawable.falafel,"falafel",40);
        myDB.addProduct(6, R.drawable.fishandchips,"fish and chips",100);
        myDB.addProduct(7, R.drawable.icecream,"ice cream",25);
        myDB.addProduct(8, R.drawable.pitzaandchips,"pitza and chips",80);
        myDB.addProduct(9, R.drawable.pitzaxl,"pitza xl",70);
        myDB.addProduct(10, R.drawable.salomoneggs,"salomon eggs",170);
        OrderId++;
        myDB.addOrder(1,"orit",new Date().toString(),new Date().toString(),4,3,150,"false");
        myDB.addOrder(2,"miri",new Date().toString(),new Date().toString(),4,3,150,"false");
        myDB.close();
    }

    public void clickRes(View view) {
        Intent intent = new Intent();
        intent.setClass(this, ReserountMap.class);
        intent.putExtra("degel", 1);
        startActivity(intent);
    }

    public void clickN(View view) {
        Intent intent = new Intent();
        intent.setClass(this, newOrder.class);
        intent.putExtra("degel", 1);

        intent.putExtra("id",OrderId);
        startActivity(intent);
    }
    public void clickO(View view){
        Intent intent = new Intent();
        intent.setClass(this, OrdersActivity.class);

        startActivity(intent);
    }

}




