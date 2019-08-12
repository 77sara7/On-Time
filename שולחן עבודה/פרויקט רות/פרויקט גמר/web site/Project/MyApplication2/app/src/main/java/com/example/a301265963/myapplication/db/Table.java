package com.example.a301265963.myapplication.db;

/**
 * Created by 315561779 on 19/04/2017.
 */

public class Table {
    int id;
    int places;

    public int getPlaces() {
        return places;
    }

    public Table(int id, int places, int isFull) {
        this.id = id;
        this.places = places;
        this.isFull = isFull;
    }

    public void setPlaces(int places) {
        this.places = places;
    }

    public int isFull() {
        return isFull;
    }

    public void setFull(int full) {
        isFull = full;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    int isFull;
}
