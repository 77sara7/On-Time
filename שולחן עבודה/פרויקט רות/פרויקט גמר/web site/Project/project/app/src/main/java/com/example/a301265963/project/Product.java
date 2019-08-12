package com.example.a301265963.project;

import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;


public class Product extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_product);
    }

    int amount;
    int idP;
    int price;
    int image;
    String name;

    public Product(int _id, int _price, String _name, int _image) {
        idP = _id;
        price = _price;
        image = _image;
        name = _name;
    }

    public int getAmount() {
        return amount;
    }

    public int getIdP() {
        return idP;
    }

    public void setAmount(int amount) {
        this.amount = amount;
    }

    public void setIdP(int idP) {
        this.idP = idP;
    }

    public int getPrice() {
        return price;
    }

    public void setImage(int image) {
        this.image = image;
    }

    public int getImage() {
        return image;
    }

    public void setName(String name) {
        this.name = name;
    }

    public void setPrice(int price) {
        this.price = price;
    }

    public String getName() {
        return name;
    }
}
