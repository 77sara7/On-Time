package com.example.a301265963.project;

/**
 * Created by 318200318 on 19/04/2017.
 */

public class Table {
    int id;
    int places;
    boolean isFull;

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public int getPlaces() {
        return places;
    }
    public boolean getIsFull() {
        return isFull;
    }
    public void setPlaces(int places) {
        this.places = places;
    }

    public void setFull(boolean full) {
        isFull = full;

    }
    public Table(int _id,int _places,boolean _isFull){
        id=_id;
        places=_places;
        isFull=_isFull;
    }
}
