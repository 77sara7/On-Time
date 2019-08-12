package com.example.a301265963.project.orders;

import android.support.annotation.Nullable;
import android.content.Context;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ListView;
import android.support.v4.app.Fragment;
import com.example.a301265963.project.myDB;
import com.example.a301265963.project.R;

import java.util.List;

/**
 * A simple {@link Fragment} subclass.
 */


    public  class OrdersListFragment extends Fragment {


        Context c;
        myDB myDB;
        public OrdersListFragment(){}
        @Override
        public void onAttach(Context context) {
            super.onAttach(context);
            c=context;
        }

        @Override
        public void onCreate(Bundle savedInstanceState) {
            super.onCreate(savedInstanceState);
        }

        @Override
        public View onCreateView(LayoutInflater inflater, ViewGroup container,
                                 Bundle savedInstanceState) {
            // Inflate the layout for this fragment
            return inflater.inflate(R.layout.fragment_orders_list, container, false);
        }

        @Override
        public void onViewCreated(View view, Bundle savedInstanceState) {
            super.onViewCreated(view, savedInstanceState);
            myDB=new myDB(c);
            List<Order> AllOrders=myDB.getAllOrders();
            AllOrdersAdapter OrdersAdapter=new AllOrdersAdapter(this.getContext(),AllOrders);
            ListView listView=(ListView) getView().findViewById(R.id.ordersList);
            listView.setAdapter(OrdersAdapter);

        }


    }

