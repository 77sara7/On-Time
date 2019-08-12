package com.example.a301265963.project.orders;


import android.content.Context;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ListView;
import android.support.v4.app.Fragment;
import com.example.a301265963.project.myDB;
import com.example.a301265963.project.R;
import com.example.a301265963.project.Table;
import com.example.a301265963.project.orders.ExistingOrdersAdapter;
import com.example.a301265963.project.orders.Order;

import java.util.List;

/**
 * Created by 318200318 on 27/04/2017.
 */
public class existFregment extends Fragment {

    int de;
    myDB myDB;
    Context c=null;
    public  existFregment(){

    }

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
        return inflater.inflate(R.layout.fragment_exists, container, false);
    }

    @Override
    public void onViewCreated(View view, Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        myDB=new myDB(c);
        List<Order> existOrders=myDB.getAllOrders();
        List<Table>  Tables=myDB.getAllTables();
        for (Order o:existOrders) {
            for (Table t:Tables) {
                if(o.TableId==t.getId() && !t.getIsFull())
                    existOrders.remove(o);
            }

        }

        ListView listView=(ListView)getView().findViewById(R.id.ExistingOrdersList);
       ExistingOrdersAdapter existingOrdersAdapter=new ExistingOrdersAdapter(this.getContext(),existOrders);
        listView.setAdapter(existingOrdersAdapter);

    }

}
