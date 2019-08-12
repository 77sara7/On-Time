  package com.example.a301265963.myapplication.db;

/**
 * Created by 315561779 on7
 */

public class Product {
    public int getpId() {
        return pId;
    }

    public void setpId(int pId) {
        this.pId = pId;
    }

    public int getpamount() {
        return amount;
    }

    public void setpamount(int amount) {
        this.amount = amount;
    }

    public String getProductName() {
        return productName;
    }

    public void setProductName(String productName) {
        this.productName = productName;
    }

    public int getProductPrice() {
        return productPrice;
    }

    public void setProductPrice(int productPrice) {
        this.productPrice = productPrice;
    }

    public int getpImageId() {
        return pImageId;
    }

    public void setpImageId(int pImageId) {
        this.pImageId = pImageId;
    }

    public Product(int pId, String productName, int pImageId, int productPrice,int amount) {
        this.pId = pId;
        this.productName = productName;
        this.pImageId = pImageId;
        this.productPrice = productPrice;
        this.amount=amount;
    }

    int pId;
    String productName;
    int productPrice;
    int pImageId;
    int amount;
}
