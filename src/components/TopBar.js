import React, { useEffect, useState } from 'react';
import { getRooms, createRoom } from "../services/roomService";
import RoomForm from "./Rooms/RoomForm";
import './TopBar.css'; // Импортируем стили

const TopBar = ({ onSelectRoom }) => {
    const [rooms, setRooms] = useState([]);
    const [isFormVisible, setIsFormVisible] = useState(false);

    const testRooms = [
        {
            id: "1",
            name: "Dining room"
        },
        {
            id: "2",
            name: "Living room"
        },
        {
            id: "3",
            name: "Kitchen"
        }
    ];

    useEffect(() => {
        const fetchRooms = async () => {
            // const data = await getRooms({});
            setRooms(testRooms);
            // setRooms(data);
        };
        fetchRooms();
    }, []);

    const handleAddRoom = (room) => {
        // Здесь вы можете добавить логику для создания комнаты
        // await createRoom(room);
        setRooms((prevRooms) => [...prevRooms, room]);
        setIsFormVisible(false); // Скрываем форму после добавления
    };

    return (
        <div className="topbar-container">
            {rooms.map((room) => (
                <button
                    key={room.id}
                    onClick={() => onSelectRoom(room)}
                    className="room-button"
                >
                    {room.name}
                </button>
            ))}
            <button
                onClick={() => setIsFormVisible(!isFormVisible)}
                className="add-room-button"
            >
                +
            </button>
            {isFormVisible && (
                <RoomForm onAddRoom={handleAddRoom} onClose={() => setIsFormVisible(false)} />
            )}
        </div>
    );
};

export default TopBar;