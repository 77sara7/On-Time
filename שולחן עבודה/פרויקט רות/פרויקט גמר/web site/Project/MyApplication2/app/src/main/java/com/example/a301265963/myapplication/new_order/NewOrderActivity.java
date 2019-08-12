package com.example.a301265963.myapplication.new_order;

import android.app.NotificationManager;
import android.app.PendingIntent;
import android.content.Intent;
import android.content.SharedPreferences;
import android.net.Uri;
import android.os.Bundle;
import android.os.Handler;
import android.preference.PreferenceManager;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.app.NotificationCompat;
import android.view.View;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.NumberPicker;
import android.widget.TextView;

import com.example.a301265963.myapplication.MyAdapter;
import com.example.a301265963.myapplication.R;
import com.example.a301265963.myapplication.db.MyDB;
import com.example.a301265963.myapplication.table.TablesActivity;

public class NewOrderActivity extends AppCompatActivity {

    NotificationManager manager = null;
    TextView textView = null;
    TextView textView2 = null;
    EditText editText = null;
    NumberPicker numberPicker = null;
    MyDB myDB = null;
    int id = -1;
    int count = 0;
    int idOrder=0;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_new_order);
        manager = (NotificationManager) getSystemService(NOTIFICATION_SERVICE);

        numberPicker = (NumberPicker) findViewById(R.id.numberPicker);
        numberPicker.setMaxValue(30);
        numberPicker.setMinValue(0);
        ListView listView = (ListView) findViewById(R.id.ls);
        myDB = new MyDB(getApplicationContext());
        myDB.addProduct(1, "expresso", 30, R.drawable.pic1);
        myDB.addProduct(2, "ice cream", 50, R.drawable.pic2);
        myDB.addProduct(3, "coffee", 30, R.drawable.pic3);
        myDB.addProduct(4, "milk shake", 40, R.drawable.pic4);
        myDB.addProduct(5, "toast", 50, R.drawable.pic5);
        myDB.addProduct(6, "cake", 100, R.drawable.pic6);
        myDB.addProduct(7, "pizza", 90, R.drawable.pic7);
        MyAdapter myAdapter = new MyAdapter(myDB.getProducts(), this);
        listView.setAdapter(myAdapter);
        textView = (TextView) findViewById(R.id.collectivePrice);
        textView2 = (TextView) findViewById(R.id.col);
        editText = (EditText) findViewById(R.id.name1);
        //= getSharedPreferences("mysp",MODE_APPEND);

    }

    public void updatePrice(int price, int price2) {
        textView.setText(Integer.parseInt(textView.getText().toString()) - price + price2 + "");
    }

    public void serchTable(View view) {

        Intent intent = new Intent();
        intent.putExtra("from", "order");
        intent.putExtra("num", numberPicker.getValue());
        intent.setClass(this, TablesActivity.class);
        startActivityForResult(intent, 11);

    }

    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);

        if (requestCode == 11) {

            if (resultCode == RESULT_OK) {
                id = data.getIntExtra("tableId", 1);
                textView2.setText("the table id is: " + id);
            }
        }
    }

    public void ok(View view) {
        boolean flag = true;

        if (id == -1) {
            flag = false;
            textView2.setText(R.string.cooseNum);

        }
        if (editText.getText().toString().isEmpty()) {
            flag = false;
            editText.setError("הכנס את שמך");

        }
        if (textView.getText().equals("0")) {
            flag = false;
            textView2.setText("בחר מוצרים להזמנה");
        }

        if (numberPicker.getValue() == 0) {
            flag = false;
            textView2.setText(R.string.coooseChair);
        }
        if (flag == true) {
            textView2.setText(R.string.invitT);
            myDB.addOrder(editText.getText().toString(), id, numberPicker.getValue()
                    , Integer.parseInt(textView.getText().toString()));
            idOrder = myDB.getOrders().get(myDB.getOrders().size() - 1).getOrderId();
            myDB.getOrders().get(myDB.getOrders().size() - 1).getName();
            myDB.updateTable(id, 1);
            waitSomeTime(10000);
            //myDB.add
        }
    }

    private void waitSomeTime(int i) {
        new Handler().postDelayed(new Runnable() {
            @Override
            public void run() {
                if(myDB.getOrders().get(idOrder-1).isServed()==0){
                NotificationCompat.Builder builder = new NotificationCompat.Builder(getApplicationContext());
                builder.setContentTitle("misada");
                builder.setContentText(myDB.getOrders().get(idOrder-1).getName()+"  הזמנתך לא נשלחה " );
                builder.setSmallIcon(R.drawable.anim);
                builder.setNumber(0);
                manager.notify(count++, builder.build());}
            }
        }, i);
    }
}
