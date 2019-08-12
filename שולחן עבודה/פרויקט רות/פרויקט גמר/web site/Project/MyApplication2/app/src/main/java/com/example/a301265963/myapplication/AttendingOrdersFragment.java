package com.example.a301265963.myapplication;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ListView;

import com.example.a301265963.myapplication.db.MyDB;


public class AttendingOrdersFragment extends Fragment {

    ListView orderList;
    MyAdapter4 myAdapter4 =null;
    MyDB myDB=null;
    public AttendingOrdersFragment() {

    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        myDB = new MyDB(getContext());
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {

        View v= inflater.inflate(R.layout.fragment_attending_orders, container, false);
        myAdapter4=new MyAdapter4(myDB.getOrdersNotLeave(),v.getContext());
        orderList= (ListView) (v.findViewById(R.id.fregAttending));
        orderList.setAdapter(myAdapter4);
        return v;
    }


}
