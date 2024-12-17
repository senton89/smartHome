import React, {useEffect, useState} from 'react';
import {getRooms} from "../services/roomService";
import RoomForm from "./Rooms/RoomForm";

const TopBar = ( {onSelectRoom} ) => {
    const [rooms, setRooms] = useState([]);

    useEffect(() => {
        const fetchRooms = async () => {
            const data = await getRooms({});
            setRooms(data);
        };
        fetchRooms();
    }, []);

    return (
        <div className="flex space-x-4 bg-white p-4 rounded-lg shadow-md">
            {rooms.map((room) => (
                <button
                    key={room.id}
                    onClick={() => onSelectRoom(room)}
                    className="text-purple-600 hover:text-purple-800 font-semibold"
                >
                    {room.name}
                </button>
            ))}
        </div>
    );
};

export default TopBar;