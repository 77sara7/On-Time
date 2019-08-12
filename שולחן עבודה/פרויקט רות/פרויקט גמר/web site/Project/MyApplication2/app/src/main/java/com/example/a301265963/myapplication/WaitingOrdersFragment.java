package com.example.a301265963.myapplication;

import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ListView;

import com.example.a301265963.myapplication.db.MyDB;

/**
 * Created by 315561779 on 27/04/2017.
 */

    public class WaitingOrdersFragment extends Fragment {
        ListView listView=null;
        View view;
        MyAdapter3 myAdapter3=null;
        MyDB myDB =null;

        public WaitingOrdersFragment() {

        }


        @Override
        public void onCreate(Bundle savedInstanceState) {
            super.onCreate(savedInstanceState);
        }

        @Override
        public View onCreateView(LayoutInflater inflater, ViewGroup container,
                                 Bundle savedInstanceState) {

            view=inflater.inflate(R.layout.fragment_waiting_orders,container,false);
            listView= (ListView) view.findViewById(R.id.fregwaiting);
            myDB = new MyDB(getContext());
            myAdapter3 = new MyAdapter3(myDB.getOrdersNotServed(),view.getContext());
            listView.setAdapter(myAdapter3);
            return view;
        }


    }


