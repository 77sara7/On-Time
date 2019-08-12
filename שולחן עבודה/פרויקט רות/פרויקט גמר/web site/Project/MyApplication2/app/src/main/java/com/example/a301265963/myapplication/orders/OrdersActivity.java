package com.example.a301265963.myapplication.orders;

import android.os.Bundle;
import android.support.design.widget.NavigationView;
import android.support.v4.app.Fragment;
import android.support.v7.app.AppCompatActivity;
import android.view.MenuItem;
import android.view.animation.Animation;
import android.view.animation.AnimationUtils;
import android.widget.FrameLayout;
import com.example.a301265963.myapplication.WaitingOrdersFragment;
import com.example.a301265963.myapplication.AllOrdersFragment;
import com.example.a301265963.myapplication.AttendingOrdersFragment;
import android.view.View;
import android.widget.ImageView;


import com.example.a301265963.myapplication.R;

public class OrdersActivity extends AppCompatActivity
        implements NavigationView.OnNavigationItemSelectedListener {
        android.app.Fragment fragment=null;
        FrameLayout frameLayout=null;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_orders);
        frameLayout= (FrameLayout) findViewById(R.id.container);
        getSupportFragmentManager().beginTransaction().add(R.id.container,new WaitingOrdersFragment()).addToBackStack(null).commit();


    }

    public void changeFragmentInContainer(Fragment fragment) {

        getSupportFragmentManager().beginTransaction().add(R.id.container,fragment).addToBackStack(null).commit();
    }

    public void changeWait(View view) {
        //getFragmentManager().beginTransaction().add(R.id.container,new WaitingOrdersFragment()).addToBackStack(null).commit();
        if( getSupportFragmentManager().getBackStackEntryCount()>0)
            getSupportFragmentManager().popBackStack();
        changeFragmentInContainer(new WaitingOrdersFragment());
    }

    public void changeAttend(View view) {
        if( getSupportFragmentManager().getBackStackEntryCount()>0)
            getSupportFragmentManager().popBackStack();
        changeFragmentInContainer(new AttendingOrdersFragment());
    }

    public void changeAll(View view) {
        if( getSupportFragmentManager().getBackStackEntryCount()>0)
            getSupportFragmentManager().popBackStack();
        changeFragmentInContainer(new AllOrdersFragment());
    }
    @Override
    public boolean onNavigationItemSelected(MenuItem item) {
        return false;
    }
}