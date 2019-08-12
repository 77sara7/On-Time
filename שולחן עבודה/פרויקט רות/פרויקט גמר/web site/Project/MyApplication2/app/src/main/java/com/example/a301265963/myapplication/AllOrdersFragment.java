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

public class AllOrdersFragment extends Fragment {

    MyAdapter5 myAdapter5 =null;
    MyDB myDB=null;
    public AllOrdersFragment() {

    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        myDB = new MyDB(getContext());
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {

        View v= inflater.inflate(R.layout.fragment_all_orders, container, false);
        myAdapter5=new MyAdapter5(myDB.getOrders(),v.getContext());
        ListView orderList= (ListView) (v.findViewById(R.id.fregAll));
        orderList.setAdapter(myAdapter5);
        return v;
    }
}
