package com.example.a301265963.myapplication;

import android.app.NotificationManager;
import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.view.animation.Animation;
import android.view.animation.AnimationUtils;
import android.widget.ImageView;

import com.example.a301265963.myapplication.new_order.NewOrderActivity;
import com.example.a301265963.myapplication.orders.OrdersActivity;
import com.example.a301265963.myapplication.table.TablesActivity;

public class MainActivity extends AppCompatActivity {


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

    }


    public void order(View view) {
        Intent intent=new Intent();
        intent.setClass(this, NewOrderActivity.class);
        startActivity(intent);
    }

    public void map(View view) {
        Intent intent=new Intent();
        intent.putExtra("from","main");
        intent.setClass(this, TablesActivity.class);
        startActivity(intent);
    }

    public void ordersLists(View view) {
        Intent intent=new Intent();
        intent.setClass(this, OrdersActivity.class);
        startActivity(intent);
    }
}
