package com.example.a301265963.project.orders;
import android.support.v4.app.Fragment;

import android.content.Context;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ListView;

import com.example.a301265963.project.myDB;
import com.example.a301265963.project.R;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by 318200318 on 27/04/2017.
 */
public class WaitingFragment extends Fragment {


    Context c;
    myDB myDB;
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
        return inflater.inflate(R.layout.activity_waiting_fregment, container, false);
    }

    @Override
    public void onViewCreated(View view, Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        myDB=new myDB(c);
        List<Order> WaitingOrder=myDB.getAllOrders();
        List<Order> waitnigs=new ArrayList<Order>();
        for (Order t:WaitingOrder)
        {
            if(!t.isServed)
               waitnigs.add(t);
        }
        ListView listView=(ListView)getView().findViewById(R.id.WaitingOrders);
        WaitingOrdersAdapter waitingOrdersAdapter=new WaitingOrdersAdapter(this.getContext(),waitnigs);
        listView.setAdapter(waitingOrdersAdapter);

    }


}
