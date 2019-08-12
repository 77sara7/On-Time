package com.example.a301265963.project;

import android.app.NotificationManager;
import android.app.PendingIntent;
import android.content.Intent;
import android.os.Bundle;
import android.os.Handler;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.app.NotificationCompat;
import android.view.View;
import android.widget.ListView;
import android.widget.NumberPicker;
import android.widget.TextView;
import android.widget.Toast;

import com.example.a301265963.project.orders.Order;

import java.util.Date;
import java.util.List;

public class newOrder extends AppCompatActivity {
    myDB myDB;
    int idOrder = 0;
    int counter=0;
    ListView listView;
    NumberPicker np;
    Order neworder ;
    Date startDate;
    int idTable=0;
    NotificationManager manager = null;
    NotificationCompat.Builder servedBuilder;
    PendingIntent pendingIntent;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
neworder=new Order(0,"",0,"",0,0);

        setContentView(R.layout.activity_new_order);
        myDB = new myDB(this);
        idOrder = getIntent().getIntExtra("id", 100);
        startDate = new Date();
        np = (NumberPicker) findViewById(R.id.numberPicker);
        np.setMaxValue(10);
        np.setMinValue(1);

        listView = (ListView) findViewById(R.id.lv);
        List<Product> allProducts = myDB.getAllProducts();
        ProductsAdapter p = new ProductsAdapter(this,allProducts);
        listView.setAdapter(p);


        manager = (NotificationManager) getSystemService(NOTIFICATION_SERVICE);
    }
    public void OK(View view) {

        final TextView tv = (TextView) findViewById(R.id.nameCustomer);
String s=tv.getText().toString();
        if ( s.isEmpty())
            Toast.makeText(this, "put your name", Toast.LENGTH_LONG).show();
        else
            myDB.addOrder(idOrder, tv.getText().toString(), startDate.toString(), new Date().toString(), np.getValue(), idTable, neworder.getTotalPrice(), "false");
        neworder.setServed(false);
        new Handler().postDelayed(
                new Runnable() {
                    @Override
                    public void run() {
                        if(neworder.getServed()==false) {

                            servedBuilder = new NotificationCompat.Builder(newOrder.this);
                            servedBuilder.setContentTitle(tv.getText() + ", sorry");
                            servedBuilder.setContentText(" your order is under treatment");
                            servedBuilder.setNumber(++counter);
                            servedBuilder.setSmallIcon(android.R.drawable.stat_notify_chat);
                            manager.notify(0, servedBuilder.build());
                        }

                    }
                }, (6000));

    }

    public void findTable(View view) {

        Intent intent = new Intent();
        intent.setClass(this, ReserountMap.class);
        int chairs = np.getValue();
        intent.putExtra("chairsNumber", chairs);
        intent.putExtra("degel",3);
        startActivityForResult(intent, 3);
    }


    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);

        if (requestCode == 3) {//back from PutDataBackActivity
            if (resultCode == RESULT_OK) {
               idTable=data.getIntExtra("TableId", 0);
                String i = "" + data.getIntExtra("TableId", 0);
                TextView t = (TextView) findViewById(R.id.chosenTable);
                t.setText(i);
            } else if (resultCode == RESULT_CANCELED) {
                Toast.makeText(this, "no chosen tables", Toast.LENGTH_LONG).show();
            }
        }
    }

    public void updateTotal(int i) {

        neworder.setTotalPrice(neworder.getTotalPrice()+i);
        TextView textView = (TextView) findViewById(R.id.totalPrice);
        String s = neworder.totalPrice + "";
        textView.setText(s);
    }
}
