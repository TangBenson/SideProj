import { React, useState, useEffect } from 'react';
import { Form, Row, Col, Select, Cascader } from 'antd';
import {
  GoogleMap,
  LoadScript,
  Marker,
  InfoWindow,
} from '@react-google-maps/api';
import { useIntl } from 'react-intl';
import { gridSettingGenerator } from 'shared/common';
import { HttpHandler } from 'shared/httpHandler';
import carLocationIcon from '../../images/Car_Location.svg';

export default function VehicleMonitoringMap() {
  const { SHOW_CHILD } = Cascader;
  const httpHandler = new HttpHandler(false);
  const translate = useIntl();

  function intlTranslate(id) {
    return translate.formatMessage({ id });
  }
  const [center, setCenter] = useState({ lat: 23.6978, lng: 120.9605 });
  //原始站點資訊
  const [originStationList, setOriginStationList] = useState([]);
  //整理過的站點資訊(for Cascader使用)
  const [stationList, setStationList] = useState([]);
  const [carList, setCarList] = useState([]);
  const [hoveredCarIndex, setHoveredCarIndex] = useState(-1);
  const [zoom, setZoom] = useState(8);

  useEffect(() => {
    const gql_station_query_str = `query {
      stationQuery {
        CityID
        CityName
        AreaID
        ZIPCode
        Area
        AreaName
        StationID
        Location
        IsNormalStation
        Latitude
        Longitude
      }
    }`;
    httpHandler
      .gql(gql_station_query_str)
      .then((response) => {
        const responseData = response.data.stationQuery;
        setStationList(transformData(responseData));
        setOriginStationList(responseData);
      })
      .catch((error) => {
        console.log(error);
      });
  }, []);

  const [form] = Form.useForm();

  const transformData = (data) => {
    let cities = {};

    for (let item of data) {
      let cityId = item['CityID'];
      let cityName = item['CityName'];
      let areaName = item['AreaName'];
      let zipCode = item['ZIPCode'];
      let location = item['Location'];
      let stationId = item['StationID'];

      // 如果城市不在cities字典中，則新增
      if (!cities[cityId]) {
        cities[cityId] = {
          label: cityName,
          value: cityId,
          children: [],
        };
      }

      // 查看是否已存在相同的區域
      let areaExists = cities[cityId].children.some(
        (area) => area.label === areaName
      );
      if (!areaExists) {
        cities[cityId].children.push({
          label: areaName,
          value: zipCode,
          children: [],
        });
      }

      // 新增站點到對應的區域
      let area = cities[cityId].children.find(
        (area) => area.label === areaName
      );
      area.children.push({
        label: location,
        value: stationId,
      });
    }

    // 轉換物件為陣列
    let citiesList = Object.values(cities);
    return citiesList;
  };

  const monitorMapSearch = (stationID) => {
    const gql_monitor_map_search_str = `query {
      monitorMap(stationID: "${stationID}") {
        CarNo
        CID
        Longitude
        Latitude
      }
    }`;
    httpHandler
      .gql(gql_monitor_map_search_str)
      .then((response) => {
        setCarList(response.data.monitorMap);
      })
      .catch(function (message) {
        alert(message);
      });
  };

  const markerIcon = {
    url: carLocationIcon,
    scaledSize: { width: 26, height: 36 },
    labelOrigin: { x: 13, y: 42 },
  };

  return (
    <div className="main-container">
      <div className="search-area">
        <Form name="search" layout="vertical" form={form}>
          <Row className="form-row">
            <Col {...gridSettingGenerator(1)}>
              <Form.Item
                label={intlTranslate(
                  'VehicleMonitoringMap.searchBar.車輛所屬據點'
                )}
              >
                <Cascader
                  options={stationList}
                  maxTagCount="responsive"
                  showCheckedStrategy={SHOW_CHILD}
                  onChange={(value) => {
                    if (value) {
                      monitorMapSearch(value[2]);
                      //找出所選站點之經緯度，設定為map中心點
                      const foundStation = originStationList.find(
                        (station) => station.StationID === value[2]
                      );
                      setCenter({
                        lat: parseFloat(foundStation.Latitude),
                        lng: parseFloat(foundStation.Longitude),
                      });
                      //zoom為控制地圖放大縮小的屬性，直接改動頁面上的zoom不會連動到自訂義狀態"zoom"，因此每次改動站點時，先硬把zoom設為10再設為11
                      setTimeout(() => {
                        setZoom(11);
                      }, 100);
                      setZoom(10);
                    }
                  }}
                />
              </Form.Item>
            </Col>
            <Col {...gridSettingGenerator(0)}>
              <LoadScript
                googleMapsApiKey={process.env.REACT_APP_GOOGLE_MAP_API_KEY}
              >
                <GoogleMap
                  mapContainerStyle={{ width: '100%', height: '700px' }}
                  center={center}
                  zoom={zoom}
                  options={{ streetViewControl: false }}
                >
                  {carList.map((item, index) => (
                    <Marker
                      key={index}
                      position={{
                        lat: parseFloat(item.Latitude),
                        lng: parseFloat(item.Longitude),
                      }}
                      title={item.CarNo}
                      icon={markerIcon}
                      label={item.CarNo}
                      onMouseOver={() => {
                        setHoveredCarIndex(index);
                      }}
                      onMouseOut={() => {
                        setHoveredCarIndex(-1);
                      }}
                    >
                      {hoveredCarIndex === index ? (
                        <InfoWindow
                          position={{
                            lat: parseFloat(item.Latitude),
                            lng: parseFloat(item.Longitude),
                          }}
                        >
                          <div>
                            {intlTranslate(
                              'VehicleMonitoringMap.searchBar.車號'
                            )}
                            : {item.CarNo}
                            <br />
                            CID: {item.CID}
                          </div>
                        </InfoWindow>
                      ) : null}
                    </Marker>
                  ))}
                </GoogleMap>
              </LoadScript>
            </Col>
          </Row>
        </Form>
      </div>
    </div>
  );
}
